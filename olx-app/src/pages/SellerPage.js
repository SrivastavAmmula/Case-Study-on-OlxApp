import React, { useState } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';

function SellerPage() {
    const [productId, setProductId] = useState('');
    const [productName, setProductName] = useState('');
    const [description, setDescription] = useState('');
    const [location, setLocation] = useState('');
    const [price, setPrice] = useState(0);
    const [categoryId, setCategoryId] = useState('');
    const userId = localStorage.getItem('userId');
    const token = localStorage.getItem('token');

    const axiosInstance = axios.create({
        baseURL: 'http://localhost:5092/api',
        headers: {
            'Authorization': `Bearer ${token}`,
        }
    });

    const handleAddProduct = async (e) => {
        e.preventDefault();

        const product = {
            productId,
            productName,
            description,
            location,
            price: parseInt(price),
            userId,
            categoryId
        };

        try {
            await axiosInstance.post('/Product/AddProduct', product);
            alert('Product added successfully');
            setProductId('');
            setProductName('');
            setDescription('');
            setLocation('');
            setPrice(0);
            setCategoryId('');
        } catch (error) {
            console.error('Error adding product:', error);
            alert('Failed to add product');
        }
    };

    return (
        <div className="container mt-5">
            <h2 className="text-center mb-4">Add Product</h2>
            <form onSubmit={handleAddProduct} className="col-md-6 mx-auto">
                <div className="mb-3">
                    <label htmlFor="productId" className="form-label">Product Id:</label>
                    <input
                        type="text"
                        className="form-control"
                        id="productId"
                        value={productId}
                        onChange={(e) => setProductId(e.target.value)}
                        required
                    />
                </div>
                <div className="mb-3">
                    <label htmlFor="productName" className="form-label">Product Name:</label>
                    <input
                        type="text"
                        className="form-control"
                        id="productName"
                        value={productName}
                        onChange={(e) => setProductName(e.target.value)}
                        required
                    />
                </div>
                <div className="mb-3">
                    <label htmlFor="description" className="form-label">Description:</label>
                    <input
                        type="text"
                        className="form-control"
                        id="description"
                        value={description}
                        onChange={(e) => setDescription(e.target.value)}
                    />
                </div>
                <div className="mb-3">
                    <label htmlFor="location" className="form-label">Location:</label>
                    <input
                        type="text"
                        className="form-control"
                        id="location"
                        value={location}
                        onChange={(e) => setLocation(e.target.value)}
                    />
                </div>
                <div className="mb-3">
                    <label htmlFor="price" className="form-label">Price:</label>
                    <input
                        type="number"
                        className="form-control"
                        id="price"
                        value={price}
                        onChange={(e) => setPrice(e.target.value)}
                        required
                    />
                </div>
                <div className="mb-3">
                    <label htmlFor="categoryId" className="form-label">Category ID:</label>
                    <input
                        type="text"
                        className="form-control"
                        id="categoryId"
                        value={categoryId}
                        onChange={(e) => setCategoryId(e.target.value)}
                        required
                    />
                </div>
                <button type="submit" className="btn btn-primary w-100">Add Product</button>
            </form>
        </div>
    );
}

export default SellerPage;
