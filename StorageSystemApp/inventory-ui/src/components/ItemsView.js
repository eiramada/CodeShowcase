import React, { useContext, useEffect, useState } from "react";
import { Alert } from "react-bootstrap";
import { StorageContext } from "../contexts/StorageLevelContext";
import { itemService } from "../services/itemService";
import ItemFormModal from "./ItemFormModal";

function mapAllItemsOnStorageLevels(storageLevels) {
  if (!Array.isArray(storageLevels) || storageLevels.length === 0) {
    return [];
  }

  return storageLevels.reduce(
    (acc, level) =>
      acc.concat(
        (level.items || []).map((item) => ({
          ...item,
          storageName: level.name,
        }))
      ),
    []
  );
}

const ItemsView = ({ userId }) => {
  const [search, setSearch] = useState("");
  const [items, setItems] = useState([]);
  const [editingItem, setEditingItem] = useState(null);
  const [showEditModal, setShowEditModal] = useState(false);
  const { storageLevels, fetchStorageLevels, loading, error } =
    useContext(StorageContext);

  useEffect(() => {
    if (storageLevels && storageLevels.length > 0) {
      const allItems = mapAllItemsOnStorageLevels(storageLevels);
      setItems(allItems);
    }
  }, [storageLevels]);

  const handleSearchChange = (event) => {
    setSearch(event.target.value);
  };

  const handleSearch = async (event) => {
    event.preventDefault();
    if (search.trim()) {
      const result = await itemService.searchItems(search);
      setItems(
        result.data.map((item) => ({
          ...item,
          storageName:
            storageLevels.find(
              (sl) => sl.storageLevelId === item.storageLevelId
            )?.name || "No Storage",
        }))
      );
    } else {
      const allItems = mapAllItemsOnStorageLevels(storageLevels);
      setItems(allItems);
    }
  };

  useEffect(() => {
    fetchStorageLevels(userId);
  }, [userId, fetchStorageLevels]);

  const handleEdit = (item) => {
    setShowEditModal(true);
    setEditingItem(item);
  };

  const handleCloseEditModal = () => {
    setShowEditModal(false);
    setEditingItem(null);
    fetchStorageLevels();
  };

  return (
    <div className="container my-3">
      <div className="input-group mb-3">
        <input
          type="text"
          className="form-control"
          placeholder="Search items"
          value={search}
          onChange={handleSearchChange}
        />
        <div className="input-group-append">
          <button className="btn btn-primary" onClick={handleSearch}>
            Search
          </button>
        </div>
      </div>

      {loading && <p>Loading...</p>}
      {error && <Alert variant="danger">{JSON.stringify(error)}</Alert>}

      <table className="table mt-3">
        <thead>
          <tr>
            <th>Title</th>
            <th>Serial Number</th>
            <th>Quantity</th>
            <th>Description</th>
            <th>Category</th>
            <th>Storage</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {items.map((item) => (
            <tr key={item.itemId}>
              <td>{item.title}</td>
              <td>{item.serialNumber}</td>
              <td>{item.quantity}</td>
              <td>{item.description}</td>
              <td>{item.category}</td>
              <td>{item.storageName}</td>
              <td>
                <button
                  className="btn btn-info"
                  onClick={() => handleEdit(item)}
                >
                  Edit
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      {showEditModal && (
        <ItemFormModal
          itemId={editingItem?.itemId}
          onClose={handleCloseEditModal}
          show={showEditModal}
        />
      )}
    </div>
  );
};

export default ItemsView;
