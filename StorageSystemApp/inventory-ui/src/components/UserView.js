import React, { useContext, useEffect, useState } from "react";
import { Button } from "react-bootstrap";
import ItemFormModal from "../components/ItemFormModal";
import ItemsView from "../components/ItemsView";
import { StorageContext } from "../contexts/StorageLevelContext";

function UserView() {
  const [showModal, setShowModal] = useState(false);
  const [currentItemId, setCurrentItemId] = useState(null);
  const { storageLevels, fetchStorageLevels } = useContext(StorageContext);

  useEffect(() => {
    if (!storageLevels.length) {
      fetchStorageLevels();
    }
  }, [storageLevels.length, fetchStorageLevels]);

  const handleOpenModal = (itemId = null) => {
    setCurrentItemId(itemId);
    setShowModal(true);
  };

  const handleCloseModal = (refresh) => {
    if (refresh) {
      fetchStorageLevels();
    }
    setShowModal(false);
    setCurrentItemId(null);
  };

  return (
    <div>
      <Button onClick={() => handleOpenModal()}>Add New Item</Button>
      <ItemsView onEditItem={handleOpenModal} />
      {showModal && (
        <ItemFormModal
          show={showModal}
          onClose={handleCloseModal}
          itemId={currentItemId}
        />
      )}
    </div>
  );
}

export default UserView;
