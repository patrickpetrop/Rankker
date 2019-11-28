import React from "react";

export default function MovieListItem({ movie }) {
  return (
    <tr key={movie.imdbId}>
      <td>{movie.name}</td>
      <td>{movie.imdbId}</td>
      <td>
        <img
          src={"https://image.tmdb.org/t/p/w92/" + movie.tmdbPosterPath}
          alt={movie.overview}
        />
      </td>
    </tr>
  );
}
