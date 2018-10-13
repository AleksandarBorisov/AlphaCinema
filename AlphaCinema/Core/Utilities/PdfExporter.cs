using AlphaCinemaData.Models;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AlphaCinema.Core.Utilities
{
	public class PdfExporter : IPdfExporter
	{
		public void ExportUserWatchedMovies(IEnumerable<Movie> movies, string userName)
		{
			string createdOn =
					($"{userName}-{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}-" +
					$"{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}-{DateTime.Now.Millisecond}");

			string fileName = $"../../../../AlphaCinemaData/Files/PDF Reports/{createdOn}.pdf";

			PdfWriter writer = new PdfWriter(fileName);
			PdfDocument pdf = new PdfDocument(writer);
			Document document = new Document(pdf, PageSize.A4);

			document.Add(new Paragraph($"Count of watched movies by {userName} = {movies.Count()}"));
			document.Add(new Paragraph(Environment.NewLine));
			if (!movies.Any())
			{
				document.Add(new Paragraph("No movies present in the database"));
			}
			else
			{ 
				document.Add(new Paragraph("Movie info:\n\n"));
				foreach (var item in movies)
				{
					document.Add(new Paragraph($"Name: {item.Name}\r\nDescription: {item.Description}\r\n" +
						$"Release Year: {item.ReleaseYear}\r\nDuration: {item.Duration} minutes"));
					document.Add(new Paragraph(Environment.NewLine));
				}
			}
			document.Close();
			writer.Close();
		}
	}
}
