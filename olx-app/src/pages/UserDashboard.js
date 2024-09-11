import React from 'react';
import { useNavigate } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import UserImage from "../components/User.jpg";

function UserDashboard() {
    const navigate = useNavigate();

    const handleSellerClick = () => {
        navigate('/seller');
    };

    const handleBuyerClick = () => {
        navigate('/buyer');
    };

    const handleMyOrdersClick = () => {
        navigate('/my-orders');
    };

    return (
        <div className="container mt-5">
            <h1 className="text-center mb-4">User Dashboard</h1>
            <img src={UserImage} width="1100" height="400"/>
            <div className="d-flex justify-content-center">
                <button className="btn btn-primary mx-2" onClick={handleSellerClick}>Seller</button>
                <button className="btn btn-secondary mx-2" onClick={handleBuyerClick}>Buyer</button>
                <button className="btn btn-info mx-2" onClick={handleMyOrdersClick}>My Orders</button>
            </div>
        </div>
    );
}

export default UserDashboard;
