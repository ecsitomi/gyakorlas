<?php

namespace Database\Factories;

use App\Models\Book;
use Illuminate\Database\Eloquent\Factories\Factory;

/**
 * @extends \Illuminate\Database\Eloquent\Factories\Factory<\App\Models\Rental>
 */
class RentalFactory extends Factory
{
    /**
     * Define the model's default state.
     *
     * @return array<string, mixed>
     */
    public function definition(): array
    {
        $bookId = Book::all()->pluck("id"); //Book ID lekérdezés és lista létrehozás
        $startDate = fake()->dateTimeBetween('-1 year')->format("Y-m-d");
        $endDate = date("Y-m-d", strtotime($startDate, "+1 week"))

        return [
            "book_id" =>fake()->randomElement($bookId), //véletlen választ a bookID listából
            "start_date" => $startDate,
            "end_date" => $endDate
        ];
    }
}
