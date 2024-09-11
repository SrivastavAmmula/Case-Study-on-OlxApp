import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Home from './pages/Home';
import Login from './pages/Login';
import AdminDashboard from './pages/AdminDashboard';
import { AuthProvider } from './context/AuthContext';
import AddUser from './components/AddUser';

import UserDashboard from './pages/UserDashboard'; // Correct the path
import SellerPage from './pages/SellerPage'; // Correct the path
import BuyerPage from './pages/BuyerPage'; // Correct the path
import MyOrders from './components/MyOrders';
// import AddProducts from './pages/SellerPage';

function App() {
    return (
        <Router>
            <AuthProvider>
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/login" element={<Login />} />
                    <Route path='/AddUser' element={<AddUser />} />
                    <Route path="/admin/dashboard" element={<AdminDashboard />} />
                    <Route path="/user/dashboard" element={<UserDashboard />} />
                    <Route path="/seller" element={<SellerPage />} />
                    <Route path="/buyer" element={<BuyerPage />} />
                    <Route path="/my-orders" element={< MyOrders/>} />
                    {/* <Route path="/seller/add-product" element={<AddProducts />} /> */}
                </Routes>
            </AuthProvider>
        </Router>
    );
}

export default App;
