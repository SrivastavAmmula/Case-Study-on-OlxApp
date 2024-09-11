import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import RegisterImage from "../components/Register.jpg";

const AddUser = () => {
    const [userId, setUserId] = useState('');
    const [userName, setUserName] = useState('');
    const [userEmail, setUserEmail] = useState('');
    const [password, setPassword] = useState('');
    const [role, setRole] = useState('');
    const navigate = useNavigate();

    const handleSubmit = (e) => {
        e.preventDefault();
        const newUser = { userId, userName, userEmail, password, role };

        axios.post('http://localhost:5092/api/User/AddUser', newUser)
            .then(response => {
                console.log(response.data);
                navigate("/Login"); // Redirect after successful user addition
            })
            .catch(error => {
                console.error('There was an error adding the user!', error);
            });
    };

    return (
        <div className="container mt-5">
            <div className="row">
                <div className="col-md-6">
                    <img src={RegisterImage} alt="Register" className="img-fluid" />
                </div>
                <div className="col-md-6">
                    <h2 className="text-center mb-4">Registration</h2>
                    <form onSubmit={handleSubmit}>
                        <div className="form-group mb-3">
                            <label htmlFor="userId">User Id</label>
                            <input 
                                type="text"
                                className="form-control"
                                id="userId"
                                placeholder="Enter User Id"
                                value={userId}
                                onChange={(e) => setUserId(e.target.value)}
                            />
                        </div>
                        
                        <div className="form-group mb-3">
                            <label htmlFor="userName">User Name</label>
                            <input
                                type="text"
                                className="form-control"
                                id="userName"
                                placeholder="Enter User Name"
                                value={userName}
                                onChange={(e) => setUserName(e.target.value)}
                            />
                        </div>

                        <div className="form-group mb-3">
                            <label htmlFor="userEmail">Email</label>
                            <input
                                type="email"
                                className="form-control"
                                id="userEmail"
                                placeholder="Enter Email"
                                value={userEmail}
                                onChange={(e) => setUserEmail(e.target.value)}
                            />
                        </div>

                        <div className="form-group mb-3">
                            <label htmlFor="password">Password</label>
                            <input
                                type="password"
                                className="form-control"
                                id="password"
                                placeholder="Enter Password"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                            />
                        </div>

                        <div className="form-group mb-3">
                            <label htmlFor="role">Role</label>
                            <select 
                                className="form-control"
                                id="role"
                                value={role}
                                onChange={(e) => setRole(e.target.value)}
                            >
                                <option value="">Select Role</option>
                                <option value="Admin">Admin</option>
                                <option value="User">User</option>
                            </select>
                        </div>

                        <button type="submit" className="btn btn-primary w-100">Register</button>
                    </form>
                </div>
            </div>
        </div>
    );
};

export default AddUser;
