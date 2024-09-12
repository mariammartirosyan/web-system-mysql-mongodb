using System;
using System.Data;
using ApplicationCore.Entities;
using ApplicationCore.Entities.MySQL;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Reports.MySQL
{
	public class ReportGenerator
	{
        private readonly ApplicationDbContext _context;

        public ReportGenerator(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<ReportRow> Generate()
        {
            var report = new List<ReportRow>();

            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                _context.Database.OpenConnection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"Select t.Id 'Tour Id', Count(b.TourId) 'Number of bookings', t.Description 'Tour Description',
                                        t.Price 'Tour Price',at.Name 'Attraction Type Name', ta.Name 'Tourist Attraction Name',
                                        ta.Location 'Tourist Attraction Location' from Tour t
                                        Join TourTouristAttraction tta on t.Id = tta.TourId
                                        Join TouristAttraction ta on ta.Id = tta.TouristAttractionId
                                        Join AttractionTypeTouristAttraction atta on ta.Id = atta.TouristAttractionId
                                        Join AttractionType at on at.Id = atta.AttractionTypeId
                                        Join Booking b on b.TourId = t.Id
                                        Where at.Name = 'Natural Landscape' and t.Price < 500
                                        Group by t.Id, ta.Name,ta.Location, t.Price, at.Name
                                       Order by Count(b.TourId) desc, t.Id desc";
                

                using (var res = cmd.ExecuteReader())
                {
                    while (res.Read())
                    {
                        var row = new ReportRow();
                        row.TourId = int.Parse(res["Tour Id"].ToString());
                        row.BookingsNumber = int.Parse(res["Number of bookings"].ToString());
                        row.Description = res["Tour Description"].ToString();
                        row.Price = decimal.Parse(res["Tour Price"].ToString());
                        row.AttractionTypeName = res["Attraction Type Name"].ToString();
                        row.AttractionName = res["Tourist Attraction Name"].ToString();
                        row.AttractionLocation = res["Tourist Attraction Location"].ToString();
                        report.Add(row);
                    }
                }
                return report;
            }
        }
    }
}

