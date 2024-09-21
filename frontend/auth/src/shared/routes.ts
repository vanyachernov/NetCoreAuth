import LoginActions from '../features/users/actions/LoginActions.tsx';
import RegisterActions from '../features/users/actions/RegisterActions.tsx';
import UsersPage from '../components/UsersPage';
import NotFoundPage from '../components/NotFoundPage';

export const routes = {
    login: {
        path: '/login',
        component: LoginActions,
    },
    register: {
        path: '/register',
        component: RegisterActions,
    },
    users: {
        path: '/users',
        component: UsersPage,
    },
    notFound: {
        path: '/not-found',
        component: NotFoundPage,
    },
};
