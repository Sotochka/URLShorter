import axios from './api';

const fetchAndStoreUserRole = async () => {
    try {
        const response = await axios.get('/auth/get-role');
        const role = response.data.role;

        if (role) {
            localStorage.setItem('userRole', role); // Store the role in local storage
        }
    } catch (error) {
        console.error('Failed to fetch user role:', error);
    }
};

export default fetchAndStoreUserRole;