import React from 'react';
import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import Header from './components/Header';
import { routes } from "./shared/routes.ts";

const isAuthenticated = () => {
    const token = localStorage.getItem('panel');
    return !!token;
};

const App: React.FC = () => {
    return (
        <Router>
            <Header isAuthenticated={isAuthenticated()} />
            <Routes>
                <Route
                    path={routes.login.path}
                    element={isAuthenticated() ? <Navigate to={routes.users.path} replace /> : <routes.login.component />}
                />
                <Route
                    path={routes.register.path}
                    element={isAuthenticated() ? <Navigate to={routes.users.path} replace /> : <routes.register.component />}
                />

                <Route
                    path={routes.users.path}
                    element={isAuthenticated() ? <routes.users.component /> : <Navigate to={routes.login.path} replace />}
                />

                <Route
                    path={routes.notFound.path}
                    element={isAuthenticated() ? <Navigate to={routes.users.path} replace /> : <routes.notFound.component />}
                />

                <Route
                    path="*"
                    element={<routes.notFound.component />}
                />
            </Routes>
        </Router>
    );
};

export default App;
