
const uri = 'https://localhost:7200/api';

function getItems() {
    fetch(uri + "/books")
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}


function get() {
    fetch(uri2 + "/product")
        .then(response => response.json())
        .then(data => console.log(data))
        .catch(error => console.error('Unable to get items.', error));
}

function _displayItems(obj) {
    const container = document.getElementById('books');

   
    // Kiểm tra xem container có tồn tại không
    if (!container) {
        console.error('Container not found');
        return;
    }

    var books = obj.books; 
    // Tạo và thêm phần tử cho mỗi cầu thủ
    books.forEach(book => {
        const booksContainer = document.createElement('tr');
        //booksContainer.classList.add('books-profile-container');
        booksContainer.id = `books-${book.bookId}`;
        // Comment phần <img> lại tạm thời
        // booksContainer.innerHTML = `
        //     <div class="books-img">
        //         <img src="${books.imageUrl}" alt="${books.name}">
        //     </div>
        // `;

        // Thêm thông tin cầu thủ vào container
        booksContainer.innerHTML += `
                        <td id="editTitle" class="books-title table-success"> ${book.title}</td>
                        <td id="editType" class="books-type table-success"> ${book.type}</td>
                        <td id="editPrice" class="books-price table-success">${book.price}</td>
                          <td id="editPublisher" class="books-Publisher table-success">${book.publisher.name}</td>
                         <td id="editAdvance" class="books-advance table-success">${book.advance}</td>
                         <td class="table-success ">
                         <button   class="btn btn-primary edit-button" onclick = "editBook(${book.bookId})">
                            Edit
                         </button>
                         </td>
                          <td class="table-success">
                          <button   class="btn btn-danger delete-button" onclick = "deleteBook(${book.bookId})">
                            Delete
                         </button>
                      
                         </td>
                    `;

        // Thêm phần tử cầu thủ vào container
        container.appendChild(booksContainer);
       
    });

    // site.js

}

function searchBookByTitle() {
    // Get the search term from the input field
    var searchTerm = document.getElementById("searchTerm").value.trim();

    // Check if the search term is empty or null
    if (!searchTerm) {
        // Remove existing search results
        var booksContainer = document.getElementById("books");
        booksContainer.innerHTML = "";

        // If search term is empty or null, call getItems function
        getItems();
        return; // Exit the function
    }
    // Construct the API query URL (replace 'YOUR_API_ENDPOINT' with the actual API endpoint)
    var apiUrl = uri + "/books?title=" + encodeURIComponent(searchTerm);

    // Make a fetch request to the API
    fetch(apiUrl)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            // Handle the data and update the UI
            updateUI(data);
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
        });
}


function searchByPrice() {
    // Get the search term from the input field
    var minPrice = document.getElementById("minPrice").value.trim();
    var maxPrice = document.getElementById("maxPrice").value.trim();

    // Check if the search term is empty or null
    if (!minPrice && !maxPrice) {
        // Remove existing search results
        var booksContainer = document.getElementById("books");
        booksContainer.innerHTML = "";

        // If search term is empty or null, call getItems function
        getItems();
        return; // Exit the function
    }
    // Construct the API query URL (replace 'YOUR_API_ENDPOINT' with the actual API endpoint)
    var apiUrl = uri + "/books?min-price=" + encodeURIComponent(minPrice) + "&max-price=" + encodeURIComponent(maxPrice);

    // Make a fetch request to the API
    fetch(apiUrl)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            // Handle the data and update the UI
            updateUI(data);
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
        });
}

function updateUI(obj) {
    // Clear previous search results
    var container = document.getElementById("books");
    container.innerHTML = "";

    var data = obj.books;
    // Tạo và thêm phần tử cho mỗi cầu thủ
    data.forEach(book => {
        const booksContainer = document.createElement('tr');
        //booksContainer.classList.add('books-profile-container');
        booksContainer.id = `books-${book.bookId}`;
        // Comment phần <img> lại tạm thời
        // booksContainer.innerHTML = `
        //     <div class="books-img">
        //         <img src="${books.imageUrl}" alt="${books.name}">
        //     </div>
        // `;

        // Thêm thông tin cầu thủ vào container
        booksContainer.innerHTML += `
                        <td id="editTitle" class="books-title table-success"> ${book.title}</td>
                        <td id="editType" class="books-type table-success"> ${book.type}</td>
                        <td id="editPrice" class="books-price table-success">${book.price}</td>
                         <td id="editPublisher" class="books-Publisher table-success">${book.publisher.name}</td>
                         <td id="editAdvance" class="books-advance table-success">${book.advance}</td>
                         <td class="table-success ">
                         <button type="submit"  class="btn btn-primary edit-button" onclick = "editBook(${book.bookId})">
                            Edit
                         </button>
                         </td>
                          <td class="table-success">

                           <button   class="btn btn-danger delete-button" onclick = "deleteBook(${book.bookId})">
                            Delete
                         </button>
                         </td>
                    `;

        // Thêm phần tử cầu thủ vào container
        container.appendChild(booksContainer);

    });
}



function getOptionPub() {

    //fetch(uri + "/books")
    //    .then(response => response.json())
       
    //    .catch(error => console.error('Error fetching book types:', error));

    fetch(uri + "/publishers")
        .then(response => response.json())
        .then(data => {
            
           
           
            const selectElement = document.getElementById('editPub');
            data.forEach(pub => {
                console.log(pub)
                const option = document.createElement('option');
                option.value = pub.publisherName;
                option.textContent = pub.publisherName;
                selectElement.appendChild(option);
            });
        })
        .catch(error => console.error('Unable to get items.', error));
}

function editBook(bookId) {
    
    var apiUrl = uri + "/books/" + bookId;
    console.log(apiUrl)
    var updatedBookData = {
        title: document.getElementById('editTitle').value,
        type: document.getElementById('editType').value,
        pubName: document.getElementById('editPub').value,
        price: parseFloat(document.getElementById('editPrice').value),
        advance: parseFloat(document.getElementById('editAdvance').value)
    };
   
    fetch(apiUrl, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(updatedBookData)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to edit book');
            }
            return response.json();
        })
        .then(data => {
            console.log('Book edited successfully:', data);
            var booksContainer = document.getElementById("books");
            booksContainer.innerHTML = "";
            // Optionally, you can update the UI after editing the book
            // For example, call a function to refresh the book list
            getItems();
            return;
        })
        .catch(error => {
            console.error('Error editing book:', error);
        });
}


function createBook() {
    var apiUrl = uri + "/books";
    
    var newBookData = {
        title: document.getElementById('editTitle').value,
        type: document.getElementById('editType').value,
        pubName: document.getElementById('editPub').value,
        price: parseFloat(document.getElementById('editPrice').value),
        advance: parseFloat(document.getElementById('editAdvance').value)
    };
    console.log(newBookData)
    fetch(apiUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(newBookData)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to create book');
            }
            return response.json();
        })
        .then(data => {
            console.log('Book created successfully:', data);
            var booksContainer = document.getElementById("books");
            booksContainer.innerHTML = "";
            // Optionally, you can update the UI after editing the book
            // For example, call a function to refresh the book list
            getItems();
            return;
        })
        .catch(error => {
            console.error('Error creat book:', error);
        });
}

function deleteBook(bookId) {
    // Replace 'your-api-endpoint' with the actual endpoint of your API
    const apiUrl = uri + "/books/" + bookId;

    fetch(apiUrl, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
            // Add any additional headers if required
        },
        // Add any body data if required
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            // Book deleted successfully
            console.log('Book deleted successfully');
            // You can perform any additional actions here, such as updating UI
          
            var booksContainer = document.getElementById("books");
            booksContainer.innerHTML = "";
            // Optionally, you can update the UI after editing the book
            // For example, call a function to refresh the book list
            getItems();
            return;
        })
        .catch(error => {
            console.error('There was a problem with your fetch operation:', error);
            // Handle error gracefully, e.g., show error message to the user
        });
}


function checkLogin() {
    const apiUrl = uri + "/user/login"
    email = document.getElementById("email").value;
    password = document.getElementById("password").value;

    console.log(email + "," + password);
    fetch(apiUrl, { // Replace with your server-side login endpoint URL
        method: 'POST', // Adjust method based on your server's requirements
        headers: {
            'Content-Type': 'application/json' // Send data as JSON
        },
        body: JSON.stringify({ email, password }) // Send email and password in request body
    })
        .then(response => {
            
            if (!response.ok) {
                throw new Error('Login failed'); // Handle login failure on client-side
            }
            console.log(response.status);
            if (response.status == 200) { // Replace with your actual success indicator name
                alert('Login succes. Please check your email and password.');
                
                window.location.href = '/Index'; // Redirect to success page on success
              
            } else {
                alert('Login failed. Please check your email and password.');
            }
            
            return response.json(); // Response might contain success indicator or session ID
        })
       
        .catch(error => {
            console.error('Error logging in:', error);
            // Handle login error gracefully (display error message to user)
        });
}

function getPub() {
    fetch(uri + "/publishers")
        .then(response => response.json())
        .then(data => _displayPubs(data))
        .catch(error => console.error('Unable to get publishers.', error));
}


function _displayPubs(pubs) {
    const container = document.getElementById('pub');


    // Kiểm tra xem container có tồn tại không
    if (!container) {
        console.error('Container not found');
        return;
    }
   

    // Tạo và thêm phần tử cho mỗi cầu thủ
    pubs.forEach(pub => {
        const pubssContainer = document.createElement('tr');
        //booksContainer.classList.add('books-profile-container');
        pubssContainer.id = `pubs-${pub.pubId}`;
        // Comment phần <img> lại tạm thời
        // booksContainer.innerHTML = `
        //     <div class="books-img">
        //         <img src="${books.imageUrl}" alt="${books.name}">
        //     </div>
        // `;

        // Thêm thông tin cầu thủ vào container
        pubssContainer.innerHTML += `
                        <td id="editPubName" class="books-title table-success"> ${pub.publisherName}</td>
                        <td id="editEmail" class="books-type table-success"> ${pub.emailAddress}</td>
                        <td id="editCity" class="books-price table-success">${pub.city}</td>
                          <td id="editState" class="books-Publisher table-success">${pub.state}</td>
                         <td id="editCountry" class="books-advance table-success">${pub.country}</td>        
                          <td class="table-success">
                          <button type="submit"  class="btn btn-primary edit-button" onclick = "editPub(${pub.pubId})">
                            Edit                    
                         </td>
                    `;

        // Thêm phần tử cầu thủ vào container
        container.appendChild(pubssContainer);

    });

    // site.js

}




function editPub(pubId) {

    var apiUrl = uri + "/publishers/" + pubId;
    console.log(apiUrl)
    var updatedPubData = {
        publisherName: document.getElementById('editPubName').value,
        emailAddress: document.getElementById('editEmail').value,
        city: document.getElementById('editCity').value,
        state: document.getElementById('editState').value,
        country: document.getElementById('editCountry').value
    };

    fetch(apiUrl, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(updatedPubData)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to edit Pub');
            }
            return response.json();
        })
        .then(data => {
            console.log('Pub edited successfully:', data);
            var pubsContainer = document.getElementById("pub");
            pubsContainer.innerHTML = "";
            // Optionally, you can update the UI after editing the book
            // For example, call a function to refresh the book list
            getPub();
            return;
        })
        .catch(error => {
            console.error('Error editing pub:', error);
        });
}