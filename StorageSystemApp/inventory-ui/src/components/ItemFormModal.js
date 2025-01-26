import React, { useContext, useEffect, useState } from "react";
import { Alert, Button, Form, InputGroup, Modal } from "react-bootstrap";
import { StorageContext } from "../contexts/StorageLevelContext";
import { itemService } from "../services/itemService";
import AddStorageLevelModal from "./AddStorageLevelModal";

const initialItemState = {
  title: "",
  serialNumber: "",
  quantity: "",
  description: "",
  category: "",
  storageLevelId: "",
};

const ItemFormModal = ({ show, onClose, itemId }) => {
  const [item, setItem] = useState(initialItemState);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState("");
  const { storageLevels, fetchStorageLevels } = useContext(StorageContext);
  const [showAddLevelModal, setShowAddLevelModal] = useState(false);

  useEffect(() => {
    if (itemId) {
      setIsLoading(true);
      itemService
        .getItem(itemId)
        .then((response) => {
          setItem(response.data);
          setIsLoading(false);
        })
        .catch((error) => {
          console.error("Error fetching item details:", error);
          setError("Failed to fetch item details");
          setIsLoading(false);
          setItem(initialItemState);
        });
    } else {
      setItem(initialItemState);
    }
  }, [itemId, fetchStorageLevels]);

  const handleChange = (event) => {
    const { name, value } = event.target;
    setItem((prev) => ({ ...prev, [name]: value }));
  };

  const handleAddStorageLevel = () => {
    setShowAddLevelModal(true);
  };

  const handleCloseAddLevelModal = () => {
    setShowAddLevelModal(false);
    fetchStorageLevels();
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    setIsLoading(true);
    try {
      const response = itemId
        ? await itemService.updateItem(itemId, item)
        : await itemService.createItem(item);
      onClose(true);
    } catch (error) {
      setError(`Failed to ${itemId ? "update" : "create"} item`);
    } finally {
      setIsLoading(false);
      fetchStorageLevels();
    }
  };

  return (
    <Modal show={show} onHide={() => onClose(false)}>
      <Form onSubmit={handleSubmit}>
        <Modal.Header closeButton>
          <Modal.Title>{itemId ? "Edit Item" : "Add Item"}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          {isLoading && <p>Loading...</p>}
          {error && <Alert variant="danger">{JSON.stringify(error)}</Alert>}

          <Form.Group className="mb-3">
            <Form.Control
              type="text"
              placeholder="Title"
              name="title"
              value={item.title}
              onChange={handleChange}
              required
            />
          </Form.Group>
          <Form.Group className="mb-3">
            <Form.Control
              type="text"
              name="serialNumber"
              placeholder="Serial Number"
              value={item.serialNumber}
              onChange={handleChange}
            />
          </Form.Group>
          <Form.Group className="mb-3">
            <Form.Control
              type="number"
              name="quantity"
              placeholder="Quantity"
              value={item.quantity}
              onChange={handleChange}
              required
            />
          </Form.Group>
          <Form.Group className="mb-3">
            <Form.Control
              as="textarea"
              name="description"
              placeholder="Description"
              value={item.description}
              onChange={handleChange}
            />
          </Form.Group>
          <Form.Group className="mb-3">
            <Form.Control
              type="text"
              name="category"
              placeholder="Category"
              value={item.category}
              onChange={handleChange}
            />
          </Form.Group>
          <InputGroup className="mb-3">
            <Form.Control
              as="select"
              name="storageLevelId"
              value={item.storageLevelId}
              onChange={handleChange}
              required
            >
              <option value="">-- Select Storage --</option>
              {storageLevels.map((level) => (
                <option key={level.storageLevelId} value={level.storageLevelId}>
                  {level.name}
                </option>
              ))}
            </Form.Control>
            <Button variant="outline-secondary" onClick={handleAddStorageLevel}>
              +
            </Button>
          </InputGroup>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="success" type="submit">
            {itemId ? "Save Changes" : "Create Item"}
          </Button>
          <Button variant="secondary" onClick={() => onClose(false)}>
            Cancel
          </Button>
        </Modal.Footer>
      </Form>
      {showAddLevelModal && (
        <AddStorageLevelModal
          show={showAddLevelModal}
          onClose={handleCloseAddLevelModal}
          onAddSuccess={fetchStorageLevels}
        />
      )}
    </Modal>
  );
};

export default ItemFormModal;
