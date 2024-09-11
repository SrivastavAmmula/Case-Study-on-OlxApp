// context/AuthContext.js
import { createContext, useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const navigate = useNavigate();

    const login = (userData) => {
        setUser(userData);
        if (userData.role === 'Admin') {
            localStorage.setItem('userId',userData.userId)
            localStorage.setItem('token', userData.token);
            navigate('/admin/dashboard');
        } else {
            localStorage.setItem('userId',userData.userId)
            localStorage.setItem('token', userData.token);
            navigate('/user/dashboard');
        }
    };

    const logout = () => {
        setUser(null);
        navigate('/');
    };

    return (
        <AuthContext.Provider value={{ user, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
};

export default AuthContext;
