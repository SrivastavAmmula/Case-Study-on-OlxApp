import React, { useState, useEffect } from "react";
import axios from "axios";
import "bootstrap/dist/css/bootstrap.min.css";
import BuyerImage from "../components/Buyer.jpg";

const BuyerPage = () => {
  const [categories, setCategories] = useState([]);
  const [selectedCategory, setSelectedCategory] = useState("");
  const [products, setProducts] = useState([]);
  const [selectedProduct, setSelectedProduct] = useState(null);
  const [orderData, setOrderData] = useState(null);
  const [paymentMethod, setPaymentMethod] = useState("Credit Card");
  const [orderId, setOrderId] = useState(null);
  const [orderDetails, setOrderDetails] = useState(null);

  useEffect(() => {
    axios
      .get("http://localhost:5092/api/Category/AllCategories")
      .then((response) => {
        setCategories(response.data);
      })
      .catch((error) => {
        console.error("There was an error fetching the categories!", error);
      });
  }, []);

  const handleCategoryChange = (event) => {
    const categoryId = event.target.value;
    setSelectedCategory(categoryId);
    if (categoryId) {
      axios
        .get(
          `http://localhost:5092/api/Product/ProductbyCategoryId?categoryId=${categoryId}`,
          {
            headers: {
              Authorization: `Bearer ${localStorage.getItem("token")}`,
            },
          }
        )
        .then((response) => {
          setProducts(response.data);
        })
        .catch((error) => {
          console.error("There was an error fetching the products!", error);
        });
    } else {
      setProducts([]);
    }
  };

  const handleProductChange = (event) => {
    const productId = event.target.value;
    setSelectedProduct(productId);
    if (productId) {
      axios
        .get(`http://localhost:5092/api/Product/ProductId?id=${productId}`, {
          headers: {
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
        })
        .then((response) => {
          setOrderData(response.data);
        })
        .catch((error) => {
          console.error(
            "There was an error fetching the product details!",
            error
          );
        });
    } else {
      setOrderData(null);
    }
  };

  const handlePlaceOrder = () => {
    const userId = localStorage.getItem("userId");

    if (selectedProduct && orderData && userId) {
      const orderPayload = {
        productId: selectedProduct,
        userId: userId,
        orderDate: new Date().toISOString(),
      };

      axios
        .post("http://localhost:5092/api/Order/AddOrder", orderPayload, {
          headers: {
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
        })
        .then((response) => {
          const newOrderId = response.data.orderId;
          setOrderId(newOrderId);
          alert("Order placed successfully!");
          handleAddTransaction(newOrderId, orderData.price, userId);
        })
        .catch((error) => {
          console.error("There was an error placing the order!", error);
          alert("Failed to place the order.");
        });
    } else {
      alert(
        "Please select a product and make sure order details are available."
      );
    }
  };

  const handleAddTransaction = (orderId, amount, userId) => {
    const transactionPayload = {
      orderId: orderId,
      amount: amount,
      userId: userId,
      paymentMethod: paymentMethod,
      transactionDate: new Date().toISOString(),
    };

    axios
      .post(
        "http://localhost:5092/api/Transaction/AddTransaction",
        transactionPayload,
        {
          headers: {
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
        }
      )
      .then((response) => {
        alert("Transaction completed successfully!");
      })
      .catch((error) => {
        console.error("There was an error adding the transaction!", error);
        alert("Failed to add the transaction.");
      });
  };

  const handleViewOrderDetails = () => {
    if (orderId) {
      axios
        .get(`http://localhost:5092/api/Order/OrderID?id=${orderId}`, {
          headers: {
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
        })
        .then((response) => {
          setOrderDetails(response.data);
        })
        .catch((error) => {
          console.error(
            "There was an error fetching the order details!",
            error
          );
        });
    } else {
      alert("No order placed yet.");
    }
  };

  return (
    <div className="container mt-4">
      <div className="row">
        {/* Image Column */}
        <div className="col-md-6">
          <img src={BuyerImage} className="img-fluid" alt="Buyer" />
        </div>

        {/* Buyer Functionality Column */}
        <div className="col-md-6">
          <h1 className="mb-4">Buyer Page</h1>

          <div className="mb-3">
            <label className="form-label">Select Category:</label>
            <select
              className="form-select"
              value={selectedCategory}
              onChange={handleCategoryChange}
            >
              <option value="">-- Select Category --</option>
              {categories.map((category) => (
                <option key={category.categoryId} value={category.categoryId}>
                  {category.categooryName} {/* Corrected property name */}
                </option>
              ))}
            </select>
          </div>

          {selectedCategory && (
            <div className="mb-3">
              <label className="form-label">Select Product:</label>
              {products.map((product) => (
                <div key={product.productId} className="form-check">
                  <input
                    type="radio"
                    id={product.productId}
                    name="product"
                    value={product.productId}
                    onChange={handleProductChange}
                    className="form-check-input"
                  />
                  <label
                    htmlFor={product.productId}
                    className="form-check-label"
                  >
                    {product.productName}
                  </label>
                </div>
              ))}
            </div>
          )}

          {orderData && (
            <div className="card mb-4">
              <div className="card-body">
                <h2 className="card-title">Product Details</h2>
                <p>
                  <strong>Name:</strong> {orderData.productName}
                </p>
                <p>
                  <strong>Description:</strong> {orderData.description}
                </p>
                <p>
                  <strong>Location:</strong> {orderData.location}
                </p>
                <p>
                  <strong>Price:</strong> ${orderData.price}
                </p>

                <button className="btn btn-primary" onClick={handlePlaceOrder}>
                  Place Order
                </button>
              </div>
            </div>
          )}

          {orderId && (
            <div className="mb-3">
              <button
                className="btn btn-secondary"
                onClick={handleViewOrderDetails}
              >
                View Order Details
              </button>
            </div>
          )}

          {orderDetails && (
            <div className="card">
              <div className="card-body">
                <h2 className="card-title">Order Details</h2>
                <p>
                  <strong>Order ID:</strong> {orderDetails.orderId}
                </p>
                <p>
                  <strong>Product Name:</strong> {orderData.productName}
                </p>
                <p>
                  <strong>Total Price:</strong> ${orderData.price}
                </p>
                <p>
                  <strong>Status:</strong> Transaction completed successfully
                </p>
              </div>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default BuyerPage;
