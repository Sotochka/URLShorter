import React, { useState, useEffect } from 'react';
import axios from '../services/api'; // Import your Axios instance
import { Link } from 'react-router-dom';

interface Url {
  id: number;
  shortenedURL: string;
  originalURL: string;
  createdAt: Date;
}

const MainPage: React.FC = () => {
  const [urls, setUrls] = useState<Url[]>([]);
  const [newUrl, setNewUrl] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  // Fetch URLs from the backend on component mount
  useEffect(() => {
    const fetchUrls = async () => {
      try {
        const { data } = await axios.get<Url[]>('/url/all');
        setUrls(data);
      } catch (err) {
        console.error('Error while fetching URLs:', err);
      }
    };

    fetchUrls();
  }, []);

  // Handle new URL submission
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    setLoading(true);

    try {
      const { data: createdUrl } = await axios.post<Url>('/url/create-url', {
        OriginalUrl: newUrl,
      });
      setUrls((prevUrls) => [...prevUrls, createdUrl]); // Add the new URL to the list
      setNewUrl('');
    } catch (err) {
      setError('Failed to create URL. Please try again later.');
      console.error('Error creating URL:', err);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div style={{ maxWidth: '600px', margin: 'auto', padding: '20px'}}>
      <h1>URL Shortener</h1>
      <button onClick={() => window.location.href = '/login'} style={{marginRight:10}}>Login</button>
      <button onClick={() => window.location.href = '/register'} style={{marginRight:10}}>Register</button>
      <button onClick={() => {
        localStorage.removeItem('token');
        localStorage.removeItem('userRole');
        localStorage.removeItem('userId');
        window.location.reload();
      }}>Logout</button>
      <p>Enter a URL below to create a short version of it!</p>

      {/* Form for Adding New URL */}
      <form onSubmit={handleSubmit} style={{ marginBottom: '20px' }}>
        <div style={{ display: 'flex', gap: '10px' }}>
          <input
            type="url"
            placeholder="Enter a valid URL"
            value={newUrl}
            onChange={(e) => setNewUrl(e.target.value)}
            required
            style={{
              flex: 1,
              padding: '10px',
              fontSize: '16px',
              border: '1px solid #ccc',
              borderRadius: '4px',
            }}
          />
          <button
            type="submit"
            disabled={loading}
            style={{
              padding: '10px 20px',
              fontSize: '16px',
              backgroundColor: '#007bff',
              color: '#fff',
              border: 'none',
              borderRadius: '4px',
              cursor: loading ? 'not-allowed' : 'pointer',
              opacity: loading ? 0.6 : 1,
            }}
          >
            {loading ? 'Adding...' : 'Shorten'}
          </button>
        </div>
        {error && <p style={{ color: 'red', marginTop: '10px' }}>{error}</p>}
      </form>

      {/* List of URLs */}
      <div>
        <h2>Shortened URLs</h2>
        {urls.length === 0 ? (
          <p>No URLs created yet.</p>
        ) : (
          <ul>
            {urls.map((url) => (
              <li key={url.id} style={{ marginBottom: '10px' }}>
                <p>
                  <strong>Short URL:</strong>{' '}
                  <a href={url.shortenedURL} target="_blank" rel="noopener noreferrer">
                    {url.shortenedURL}
                  </a>
                </p>
                <p>
                  <strong>Original URL:</strong> {url.originalURL}
                </p>
                <p>
                  <strong>Created at:</strong> {new Date(url.createdAt).toLocaleString()}
                </p>
                <Link to={`/more-info/${url.id}`}>View Details</Link>
              </li>
            ))}
          </ul>
        )}
      </div>
    </div>
  );
};

export default MainPage;
