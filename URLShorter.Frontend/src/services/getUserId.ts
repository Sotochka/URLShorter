import {jwtDecode} from 'jwt-decode';

export const extractUserIdFromToken = (token: string): string | null => {
  try {
    const decodedToken: { [key: string]: any } = jwtDecode(token);
    return decodedToken['nameid'] || null; // Replace 'nameid' if necessary
  } catch (error) {
    console.error('Failed to decode JWT token', error);
    return null;
  }
};