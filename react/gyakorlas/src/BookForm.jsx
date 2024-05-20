import PropTypes from 'prop-types';
import { useRef, useState } from 'react';

function BookForm(props){
    const {onSuccess} = props;
    const titleRef = useRef();
    const authorRef = useRef();
    const publish_yearRef = useRef();
    const page_countRef = useRef();
    const url = 'http://localhost:8000/api/books';
    const [error, setError] = useState(''); //hibaüzenet lista

    const handleSubmit = event => { //mi történjen ha elküldjük az űrlapot
        event.preventDefault();
        createBook();
    };

    const createBook = async () => { //új könyv felvétele
        const book = { //objektumosítás
            title: titleRef.current.value,
            author: authorRef.current.value,
            publish_year: publish_yearRef.current.value,
            page_count: page_countRef.current.value
        }
        const response = await fetch(url, { //fetch kapcsolat
            method: 'POST',
            body: JSON.stringify(book),
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            }
        });
        if (response.ok){ //válaszok kezelése
            clearForm();
            onSuccess();
        }
        else {
            const data = await response.json();
            setError(data.message); //hibaüzenet megadása a listába

        }
    };

    const clearForm = () => { //Ürlapok megtisztítása
        titleRef.current.value = '';
        authorRef.current.value = '';
        publish_yearRef.current.value = '';
        page_countRef.current.value = '';
        setError('');

    };
    
    return (
        <form onSubmit={handleSubmit}>
            <h2>Új könyv felvétele</h2>
            { error != "" ? <div className="alert alert-danger">{error}</div> : '' } {/*HIBAÜZENET ha van*/}
            <div className="mb-3">
                <label htmlFor="title" className='form-label'>Cím</label>
                <input type="text" id='title' className='form-control' ref={titleRef}/>
            </div>
            <div className="mb-3">
                <label htmlFor="author" className='form-label'>Szerző</label>
                <input type="text" id='author' className='form-control' ref={authorRef}/>
            </div>
            <div className="mb-3">
                <label htmlFor="publish_year" className='form-label'>Kiadás éve</label>
                <input type="number" id='publish_year' className='form-control' ref={publish_yearRef}/>
            </div>
            <div className="mb-3">
                <label htmlFor="page_count" className='form-label'>Oldalak száma</label>
                <input type="number" id='title' className='form-control' ref={page_countRef}/>
            </div>
            <button className='btn btn-primary' type='submit'>Új könyv</button>
        </form>
    );
}

BookForm.propTypes = {
    onSuccess: PropTypes.func.isRequired
}

export default BookForm;