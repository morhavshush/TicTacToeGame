using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe_Models.DB_Models;

namespace TicTacToe_Data
{
	public class DB_Context : DbContext
	{
		public DB_Context(DbContextOptions<DB_Context> options)
			: base(options)
		{
			//ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		}

		public DbSet<Message> Messages { get; set; }
		public DbSet<User> Users { get; set; }
	}
}
