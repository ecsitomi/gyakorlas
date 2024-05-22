<?php

namespace App\Http\Controllers\API;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use App\Models\Book;
use App\Http\Requests\StoreBookRequest

class BookController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        $books = Book::all();
        return response()->json(["data" => $books]); //DATA elemen jönnek vissza az adatok
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(StoreBookRequest $request) //validálás a saját requestben a feladat szerint
    {
        $book = new Book($request->all());
        $book->save();
        return response()->json($book, 201);
    }

    public function rent(Request $request, string $id) //feladathoz rent! útvonal
    {
        $book == Book::find($id); //id alapján keresse meg a könyvet
        if (is_null($book)) { //ha nincs ilyen könyv
            return response()->json(["message" => "Nincs ilyen azonosító számú könyv: $id"], 404); //hibaüzenet
        }
        $rentals = Rental::where([ //nézze meg a kereséseket
            ["book_id", $id], //book_id egyenlő a keresett id-vel
            ["start_date", "<=", date("Y-m-d")], //aminek a kezdő dátuma kisebb vagy egyenlő a mai dátumnál
            ["end_date", ">", date("Y-m-d")] //vége dátuma nagyobb mint a mai
        ])->get(); //kölcsönzések lekérdezése

        if (!$rentals->isEmpty()){ //ha nem üres a rentals
            return response()->json(["message" => "A könyv jelenleg ki van kölcsönözve"], 409); //ha van ilyen könyv
        }

        //ha nincs kikölcsönözve
        $rental = new Rental();
        $rental->book_id = $id; //könyv id-ja
        $rental->start_date = date("Y-m-d"); //kezdő dátum
        $rental->end_date = date("Y-m-d", strtotime("+7 days")); //vége dátum
        $rental->save(); //mentés

        return response()->json($rental, 201) //sikeres foglalás üzenet

    }

    /**
     * Display the specified resource.
     */
    public function show(string $id)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(Request $request, string $id)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(string $id)
    {
        //
    }
}
