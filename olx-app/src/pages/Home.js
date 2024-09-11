import { Link } from 'react-router-dom';
import HomeImage from '../components/Home.jpg'

function Home() {
    return (
        <div className="container text-center mt-5">
            <h1 className="mb-4">Welcome to the OLX App</h1>
            <img src={HomeImage} width="550" height="400"/>
            
            <div className="d-flex justify-content-center">
                <Link to="/login" className="btn btn-primary mx-2">Login</Link>
                <Link to="/AddUser" className="btn btn-secondary mx-2">Register</Link>
            </div>
        </div>
    );
}

export default Home;
