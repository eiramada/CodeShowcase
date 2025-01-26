export const formattedDate = (date) => {
  let dateToObject = date;

  if (!(dateToObject instanceof Date)) {
    dateToObject = new Date(date);
  }

  return dateToObject.toLocaleDateString("et-EE", {
    day: "2-digit",
    month: "2-digit",
    year: "numeric",
  });
};
