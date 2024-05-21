<?php

use Illuminate\Support\Facades\Route;
use App\Http\Controllers\BookController;

Route::apiResource("/books", BookController::class); //minden CRUD műveletet megvalósít
Route::post("/books/{id}/rent", [BookController::class, "rent"]); //kért útvonal
