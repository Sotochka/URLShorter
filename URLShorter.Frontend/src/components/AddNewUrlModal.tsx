import React, { useState } from 'react';
import Modal from 'react-modal';

interface Props {
  isOpen: boolean;
  onClose: () => void;
  onAdd: (newUrl: string) => void;
}

const AddNewUrlModal: React.FC<Props> = ({ isOpen, onClose, onAdd }) => {
  const [url, setUrl] = useState('');

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onAdd(url);
    setUrl('');
    onClose();
  };

  return (
    <Modal isOpen={isOpen} onRequestClose={onClose}>
      <h2>Add New URL</h2>
      <form onSubmit={handleSubmit} className=''>
        <input
          type="text"
          placeholder="Enter URL"
          value={url}
          onChange={(e) => setUrl(e.target.value)}
          required
        />
        <button type="submit">Add</button>
      </form>
      <button onClick={onClose}>Cancel</button>
    </Modal>
  );
};

export default AddNewUrlModal;
