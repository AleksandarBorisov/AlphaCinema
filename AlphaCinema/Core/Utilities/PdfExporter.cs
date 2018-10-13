using AlphaCinemaData.Models;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinema.Core.Utilities
{
	public class PdfExporter : IPdfExporter
	{
		private PdfWriter writer;
		private PdfDocument pdf;
		Document document;
		public void ExportUserWatchedMovies(IEnumerable<Movie> movies, string userName)
		{
			string createdOn =
					($"{userName}-{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}-" +
					$"{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}-{DateTime.Now.Millisecond}");

			string fileName = $"../../../../AlphaCinemaData/Files/PDF Reports/Watched Movies by Users/{createdOn}.pdf";

			writer = new PdfWriter(fileName);
			pdf = new PdfDocument(writer);
			document = new Document(pdf, PageSize.A4);

			document.Add(new Paragraph($"Count of watched movies by {userName} = {movies.Count()}"));
			document.Add(new Paragraph(Environment.NewLine));
			if (!movies.Any())
			{
				document.Add(new Paragraph("No movies present in the database"));
			}
			else
			{ 
				document.Add(new Paragraph("Movie info:\n"));
				foreach (var item in movies)
				{
					document.Add(new Paragraph($"Name: {item.Name}\r\nDescription: {item.Description}\r\n" +
						$"Release Year: {item.ReleaseYear}\r\nDuration: {item.Duration} minutes"));
				}
			}
			document.Close();
			writer.Close();
		}

		public void ExportWatchedMoviesByUsers(SortedDictionary<string, HashSet<Movie>> watchedMovies)
		{
			string createdOn =
					($"{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}-" +
					$"{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}-{DateTime.Now.Millisecond}");

			string fileName = $"../../../../AlphaCinemaData/Files/PDF Reports/Watched Movies by All Users/{createdOn}.pdf";

			writer = new PdfWriter(fileName);
			pdf = new PdfDocument(writer);
			document = new Document(pdf, PageSize.A4);

			foreach (var userName in watchedMovies.Keys)
			{
				document.Add(new Paragraph($"Count of watched movies by {userName} = {watchedMovies[userName].Count}"));
				if (!watchedMovies[userName].Any())
				{
					document.Add(new Paragraph("No movies present in the database"));
					continue;
				}
				document.Add(new Paragraph("Movie info:\n"));
				foreach (var movie in watchedMovies[userName])
				{
					document.Add(new Paragraph($"Name: {movie.Name}\r\nDescription: {movie.Description}\r\n" +
						$"Release Year: {movie.ReleaseYear}\r\nDuration: {movie.Duration} minutes"));
				}
			}
			document.Close();
			writer.Close();
		}
	}
}
