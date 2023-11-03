window.openModal = (id) => document.getElementById(id).showModal();
window.closeModal = (id) => document.getElementById(id).close();

var editOrDeleteButton = document.getElementById("changeContentButton");

if (editOrDeleteButton.disabled) {
    editOrDeleteButton.title = `Элемент нельзя удалять, если:
        - эти данные нельзя удалять изначально
        - на этот элемент есть ссылки в других таблицах`;
}
