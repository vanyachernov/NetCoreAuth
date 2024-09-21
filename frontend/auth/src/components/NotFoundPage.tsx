import React from 'react';
import { Link } from 'react-router-dom';
import { routes } from '../shared/routes';

const isAuthenticated = () => {
    const token = localStorage.getItem('authToken');
    return !!token;
};

const NotFoundPage: React.FC = () => {
    return (
        <div style={{ textAlign: 'center', marginTop: '50px' }}>
            {!isAuthenticated() ? (
                <>
                    <h1>Service Unavailable</h1>
                    <p>You need to be logged in to access this service.</p>
                    <Link to={routes.login.path} style={{ color: 'blue', textDecoration: 'underline' }}>
                        Sign In
                    </Link>
                </>
            ) : (
                <>
                    <h1>404 - Page Not Found</h1>
                    <p>The page you are looking for does not exist.</p>
                </>
            )}
        </div>
    );
};

export default NotFoundPage;
