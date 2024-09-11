import React, { useState } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';

function AdminDashboard() {
    const [users, setUsers] = useState([]);
    const [showUsers, setShowUsers] = useState(false);
    const [transactions, setTransactions] = useState([]);
    const [showTransactions, setShowTransactions] = useState(false);
    const [products, setProducts] = useState([]);
    const [showProducts, setShowProducts] = useState(false);
    const [orders, setOrders] = useState([]);
    const [showOrders, setShowOrders] = useState(false);
    const [categories, setCategories] = useState([]);
    const [showCategories, setShowCategories] = useState(false);

    const [categoryId, setCategoryId] = useState('');
    const [categoryName, setCategoryName] = useState('');

    const token = localStorage.getItem('token'); 

    const axiosInstance = axios.create({
        baseURL: 'http://localhost:5092/api',
        headers: {
            'Authorization': `Bearer ${token}`,
        }
    });

    const fetchAllUsers = async () => {
        try {
            const response = await axiosInstance.get('/User/GetAllUser', {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`,
                },
            });
            setUsers(response.data);
            setShowUsers(true);
        } catch (error) {
            console.error('Error fetching users:', error);
        }
    };

    const deleteUser = async (userId) => {
        try {
            await axiosInstance.delete(`/User/DeleteUser?id=${userId}`);
            fetchAllUsers(); 
        } catch (error) {
            console.error('Error deleting user:', error);
        }
    };

    const fetchAllTransactions = async () => {
        try {
            const response = await axiosInstance.get('/Transaction/GetAllTransactions');
            setTransactions(response.data);
            setShowTransactions(true);
        } catch (error) {
            console.error('Error fetching transactions:', error);
        }
    };

    const fetchAllOrders = async () => {
        try {
            const response = await axiosInstance.get('/Order/AllOrders');
            setOrders(response.data);
            setShowOrders(true);
        } catch (error) {
            console.error('Error fetching orders:', error);
        }
    };

    const fetchAllProducts = async () => {
        try {
            const response = await axiosInstance.get('/Product/GetAllProducts');
            setProducts(response.data);
            setShowProducts(true);
        } catch (error) {
            console.error('Error fetching products:', error);
        }
    };

    const fetchAllCategories = async () => {
        try {
            const response = await axiosInstance.get('/Category/AllCategories');
            setCategories(response.data);
            setShowCategories(true);
        } catch (error) {
            console.error('Error fetching categories:', error);
        }
    };

    const addCategory = async () => {
        try {
            await axiosInstance.post('/Category/AddCategory', { categoryId, categooryName: categoryName });
            fetchAllCategories(); 
            setCategoryId('');
            setCategoryName('');
        } catch (error) {
            console.error('Error adding category:', error);
        }
    };

    const updateCategory = async () => {
        try {
            await axiosInstance.put(`/Category/EditCategory?id=${categoryId}`, { categoryId, categooryName: categoryName });
            fetchAllCategories(); 
            setCategoryId('');
            setCategoryName('');
        } catch (error) {
            console.error('Error updating category:', error);
        }
    };

    const deleteCategory = async (id) => {
        try {
            await axiosInstance.delete(`/Category/DeleteCategory?id=${id}`);
            fetchAllCategories(); 
        } catch (error) {
            console.error('Error deleting category:', error);
        }
    };

    return (
        <div className="container mt-5">
            <h1 className="text-center mb-4">Admin Dashboard</h1>
            <div className="d-flex justify-content-around mb-4">
                <button className="btn btn-primary" onClick={fetchAllUsers}>Show All Users</button>
                <button className="btn btn-info" onClick={fetchAllTransactions}>All Transactions</button>
                <button className="btn btn-warning" onClick={fetchAllOrders}>All Orders</button>
                <button className="btn btn-success" onClick={fetchAllProducts}>All Products</button>
                <button className="btn btn-secondary" onClick={fetchAllCategories}>All Categories</button>
            </div>

            {/* Display Users */}
            {showUsers && (
                <div>
                    <h2>Users</h2>
                    <ul className="list-group">
                        {users.length > 0 ? (
                            users.map(user => (
                                <li className="list-group-item d-flex justify-content-between align-items-center" key={user.userId}>
                                    <strong>Name:</strong>{user.userName} <strong>Email ID:</strong>{user.userEmail} <strong>Role:</strong> {user.role}
                                    <button className="btn btn-danger btn-sm" onClick={() => deleteUser(user.userId)}>Delete</button>
                                </li>
                            ))
                        ) : (
                            <p>No users found.</p>
                        )}
                    </ul>
                </div>
            )}

            {/* Display Transactions */}
            {showTransactions && (
                <div>
                    <h2>Transactions</h2>
                    <ul className="list-group">
                        {transactions.length > 0 ? (
                            transactions.map(transaction => (
                                <li className="list-group-item" key={transaction.transactionId}>
                                    <strong>ID:</strong> {transaction.transactionId} | 
                                    <strong>Amount:</strong> ${transaction.amount} | 
                                    <strong>Date:</strong> {new Date(transaction.transactionDate).toLocaleString()} | 
                                    <strong>Method:</strong> {transaction.paymentMethod}
                                </li>
                            ))
                        ) : (
                            <p>No transactions found.</p>
                        )}
                    </ul>
                </div>
            )}

            {/* Display Orders */}
            {showOrders && (
                <div>
                    <h2>Orders</h2>
                    <ul className="list-group">
                        {orders.length > 0 ? (
                            orders.map(order => (
                                <li className="list-group-item" key={order.orderId}>
                                    <strong>ID:</strong> {order.orderId} | 
                                    <strong>Date:</strong> {new Date(order.orderDate).toLocaleString()} | 
                                    <strong>Product ID:</strong> {order.productId} | 
                                    <strong>User ID:</strong> {order.userId}
                                </li>
                            ))
                        ) : (
                            <p>No orders found.</p>
                        )}
                    </ul>
                </div>
            )}

            {/* Display Products */}
            {showProducts && (
                <div>
                    <h2>Products</h2>
                    <ul className="list-group">
                        {products.length > 0 ? (
                            products.map(product => (
                                <li className="list-group-item" key={product.productId}>
                                    <strong>ID:</strong> {product.productId} | 
                                    <strong>Name:</strong> {product.productName} | 
                                    <strong>Description:</strong> {product.description} | 
                                    <strong>Location:</strong> {product.location} | 
                                    <strong>Price:</strong> ${product.price}
                                </li>
                            ))
                        ) : (
                            <p>No products found.</p>
                        )}
                    </ul>
                </div>
            )}

            {/* Display Categories */}
            {showCategories && (
                <div>
                    <h2>Categories</h2>
                    <ul className="list-group">
                        {categories.length > 0 ? (
                            categories.map(category => (
                                <li className="list-group-item d-flex justify-content-between align-items-center" key={category.categoryId}>
                                    <strong>ID:</strong>{category.categoryId}<strong>Category Name:</strong>{category.categooryName}
                                    <button className="btn btn-danger btn-sm" onClick={() => deleteCategory(category.categoryId)}>Delete</button>
                                </li>
                            ))
                        ) : (
                            <p>No categories found.</p>
                        )}
                    </ul>
                </div>
            )}

            {/* Add/Update Category Form */}
            <div className="mt-4">
                <h2>{categoryId ? 'Update Category' : 'Add Category'}</h2>
                <div className="form-group">
                    <input
                        type="text"
                        className="form-control mb-2"
                        placeholder="Category ID"
                        value={categoryId}
                        onChange={(e) => setCategoryId(e.target.value)}
                    />
                    <input
                        type="text"
                        className="form-control mb-2"
                        placeholder="Category Name"
                        value={categoryName}
                        onChange={(e) => setCategoryName(e.target.value)}
                    />
                    <button className="btn btn-success" onClick={addCategory}>Add Category</button>
                    {categoryId && (
                        <button onClick={updateCategory} className="btn btn-secondary ml-2">Update Category</button>
                    )}
                </div>
            </div>
        </div>
    );
}

export default AdminDashboard;
