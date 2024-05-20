import PropTypes from 'prop-types';

function BookCard(props){
    const {book} = props;
    const url = 'http://localhost:8000/api/books';

    const rentBook = async () => { //foglalás 
        const response = await fetch(url+'/'+book.id+'/rent', {
            method: 'POST',
            headers: {
                'Accept': 'application/json'
            }   
        });
        if (response.ok) {
            alert('Kölcsönzés sikeres!');
        }
        else {
            const data = await response.json();
            alert('data.message');
        }
    };

    return (
        <div className="col card">
            <div className="card-body">
                <h2>{book.title}</h2>
                <p>Kiadási év: {book.publish_year}<br/>
                Hossz: {book.page_count} oldal</p>
                <img className='img-fluid' src={"szerzok/"+book.author+".jpg"} alt={book.author} /> {/*kép betöltése*/}
                <button className='btn btn-success' style={{width:"100%"}} onClick={() => rentBook()}>Kölcsönzés</button> {/*kölcsönzés gomb*/}
            </div>
        </div>
    );
}

BookCard.propTypes = {
    book: PropTypes.object.isRequired
};

export default BookCard;