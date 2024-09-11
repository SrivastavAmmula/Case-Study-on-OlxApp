import React, { useState, useEffect } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';

const MyOrders = () => {
    const [orders, setOrders] = useState([]);

    useEffect(() => {
        const userId = localStorage.getItem('userId'); // Get userId from localStorage

        // Fetch orders by userId from the backend
        axios.get(`http://localhost:5092/api/Order/OrderByUserId?userId=${userId}`,
             {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`, // Pass token from localStorage
            },
        })
        .then(response => {
            setOrders(response.data);
        })
        .catch(error => {
            console.error('There was an error fetching the orders!', error);
        });
    }, []);

    return (
        <div className="container mt-4">
            <h1 className="mb-4">My Orders</h1>
            {orders.length > 0 ? (
                <table className="table table-striped">
                    <thead>
                        <tr>
                            <th>Order ID</th>
                            <th>Order Date</th>
                            <th>Product ID</th>
                            <th>User ID</th>
                        </tr>
                    </thead>
                    <tbody>
                        {orders.map((order) => (
                            <tr key={order.orderId}>
                                <td>{order.orderId}</td>
                                <td>{new Date(order.orderDate).toLocaleDateString()}</td> {/* Format the date */}
                                <td>{order.productId}</td>
                                <td>{order.userId}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            ) : (
                <p>No orders found.</p>
            )}
        </div>
    );
};

export default MyOrders;
