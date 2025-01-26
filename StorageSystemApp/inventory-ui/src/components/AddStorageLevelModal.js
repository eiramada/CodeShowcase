import React, { useCallback, useEffect, useState } from "react";
import { Alert, Button, Form, Modal } from "react-bootstrap";
import { storageLevelService } from "../services/storageLevelService";

function AddStorageLevelModal({ onClose, onAddSuccess, show }) {
  const [name, setName] = useState("");
  const [parentId, setParentId] = useState(null);
  const [storageLevels, setStorageLevels] = useState([]);
  const [error, setError] = useState("");

  const fetchStorageLevels = useCallback(async () => {
    try {
      const response = await storageLevelService.getAllStorageLevelsForUser();
      setStorageLevels(response.data);
    } catch (error) {
      console.error("Error fetching storage levels:", error);
    }
  }, []);

  useEffect(() => {
    fetchStorageLevels();
  }, [fetchStorageLevels]);

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      const newLevel = { name, parentId };
      await storageLevelService.createStorageLevel(newLevel);
      onAddSuccess();
      onClose();
    } catch (error) {
      console.error("Failed to add storage level:", error);
      setError("Failed to add storage level");
    }
  };

  return (
    <Modal show={show} onHide={onClose}>
      <Form onSubmit={handleSubmit}>
        <Modal.Header closeButton>
          <Modal.Title>Add Storage Level</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          {error && <Alert variant="danger">{JSON.stringify(error)}</Alert>}
          <Form.Group controlId="storageLevelName" className="mb-3">
            <Form.Control
              type="text"
              value={name}
              onChange={(e) => setName(e.target.value)}
              placeholder="Enter storage level name"
              required
            />
          </Form.Group>
          <Form.Group controlId="parentStorageLevel" className="mb-3">
            <Form.Control
              as="select"
              value={parentId || ""}
              onChange={(e) =>
                setParentId(e.target.value === "null" ? null : e.target.value)
              }
              required
            >
              <option value="null">No Parent</option>
              {storageLevels.map((level) => (
                <option key={level.storageLevelId} value={level.storageLevelId}>
                  {level.name}
                </option>
              ))}
            </Form.Control>
          </Form.Group>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="primary" type="submit">
            Add
          </Button>
          <Button variant="secondary" onClick={onClose}>
            Cancel
          </Button>
        </Modal.Footer>
      </Form>
    </Modal>
  );
}

export default AddStorageLevelModal;
