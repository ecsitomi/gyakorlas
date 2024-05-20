import "bootstrap/dist/css/bootstrap.min.css";
import { useEffect, useState } from 'react';
import BookCard from "./BookCard";
import BookForm from "./BookForm";

function App() {
  const [books, setBooks] = useState([]); //lista
  const url = 'http://localhost:8000/api/books';

  const readAllBooks = async () => { //async fetch
    const response = await fetch(url);
    const data = await response.json();
    setBooks(data.data); //mert laravelben datába raktuk a listát
  };

  useEffect(() => {
    readAllBooks();
  });

  return (
    <>
      <header>
        <div className="container">
          <nav className="navbar navbar-expand-lg navbar-light bg-light">
            <div className="container-fluid">
              <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span className="navbar-toggler-icon"></span>
              </button>
              <div clasNames="collapse navbar-collapse" id="navbarNav">
                <ul className="navbar-nav">
                  <li className="nav-item">
                    <a className="nav-link active" aria-current="page" href="#felvetel">Új könyv felvétele</a>
                  </li>
                  <li className="nav-item">
                    <a className="nav-link" href="http://petrik.hu">Petrik honlap</a>
                  </li>
                </ul>
              </div>
            </div>
          </nav>
        </div>
        <div className="container">
          <h1>Petrik Könyvtár Nyilvántartó</h1>
        </div>
      </header>

      <main className='container'>
        <div className="row row-cols-1 row-cols-md-2 row-cols-lg-3">
          {books.map(book => <BookCard key={book.id} book={book} />)} {/*könyv kártyák!!!*/}
        </div>
        <div className="mt-3" id='felvetel'>
          <BookForm onSuccess={readAllBooks} />
        </div>
      </main>

      <footer className='container'>
        <p>Készítette: Ecsedi Tamás</p>
      </footer>
    </>
  )
}

export default App
