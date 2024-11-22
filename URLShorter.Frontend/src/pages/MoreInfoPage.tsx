import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import axios from '../services/api';
import fetchAndStoreUserRole from '../services/getUserRole';

// Interfaces
interface UrlDetails {
  id: number;
  shortenedURL: string;
  originalURL: string;
  createdAt: string;
  userId: string; // Assuming userId is stored as a Guid
}

const MoreInfoPage: React.FC = () => {
  const { id } = useParams<{ id: string }>(); // Get `id` from the URL
  const [urlDetails, setUrlDetails] = useState<UrlDetails | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [userRole, setUserRole] = useState<string | null>(null);
  const [userId, setUserId] = useState<string | null>(localStorage.getItem('userId'));

  // Fetch URL details
  const fetchUrlDetails = async () => {
    try {
      setLoading(true);

      // Fetch URL details
      const response = await axios.get(`/url/${id}`); // Adjust endpoint as needed
      setUrlDetails(response.data);

      // Fetch and store the user role
      await fetchAndStoreUserRole();
      const role = localStorage.getItem('userRole');
      setUserRole(role);
    } catch (err) {
      setError('Failed to load URL details or user role. Please try again later.');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  // Delete URL function
  const deleteUrl = async () => {
    try {
      setLoading(true);
      await axios.delete(`/url/${id}`); // Adjust endpoint to delete URL
      window.location.href = '/main'; // Redirect to Main page
    } catch (err) {
      setError('Failed to delete URL. Please try again later.');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  // Fetch details on component mount
  useEffect(() => {
    if (id) {
      fetchUrlDetails();
    }
  }, [id]);

  // Render conditions
  if (loading) {
    return <p>Loading URL details...</p>;
  }

  if (error) {
    return <p>{error}</p>;
  }

  if (!urlDetails) {
    return <p>URL details not found.</p>;
  }

  // Check delete permissions
  const canDelete =
    userRole === 'Admin' || (userId && urlDetails.userId === userId);

  return (
    <div>
      <h1>URL Details</h1>
      <button onClick={() => (window.location.href = '/main')}>Main Page</button>
      <p>
        <strong>Short URL:</strong> {urlDetails.shortenedURL}
      </p>
      <p>
        <strong>Original URL:</strong> {urlDetails.originalURL}
      </p>
      <p>
        <strong>Created At:</strong> {new Date(urlDetails.createdAt).toLocaleString()}
      </p>

      {/* Conditionally display delete button */}
      {canDelete && (
        <button
          onClick={deleteUrl}
          style={{
            padding: '10px 20px',
            backgroundColor: 'red',
            color: 'white',
            border: 'none',
            borderRadius: '4px',
            cursor: 'pointer',
          }}
        >
          Delete URL
        </button>
      )}
    </div>
  );
};

export default MoreInfoPage;
