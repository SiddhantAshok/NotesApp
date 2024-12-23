const saveButton = document.querySelector('#btnSave');
const titleInput = document.querySelector('#title');
const descriptionInput = document.querySelector('#description');


function addNote(title, description){

  const body = {
    title: title,
    description : description,
    isVisible : true
  }

  fetch('https://localhost:7269/api/notes',{
    method:'POST',
    body: JSON.stringify(body),
    headers : {
      "content-type" : "application/json"
    }
  })
  .then(data => data.json())
  .then(response => console.log(response));
}


saveButton.addEventListener('click', function(){
  addNote(titleInput.value, descriptionInput.value);
})