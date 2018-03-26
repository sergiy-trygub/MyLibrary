using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLibrary.UI.Infrastructure;
using MyLibrary.UI.Services;

namespace MyLibrary.UI
{
    public partial class Form1 : Form
    {
        private readonly IAuthorRepository _authorRepo = new TempDictionaryAuthorRepository();
        private readonly IMyBookRepository _mybookRepo = new TempDictionaryMyBookRepository();
        private readonly Guid _userId = Guid.NewGuid();        
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // parameters for command from UI
            var cmdParameters = new AddBookToLibraryCommand.AddBookParameters
            {
                AuthorFirstName = txtAuthorFirstName.Text,
                AuthorLastName = txtAuthorLastName.Text,
                Description = txtDescription.Text,
                Isbn = txtIsbn.Text,
                Title = txtTitle.Text,
                UserId = _userId
            };
                        
            // command to add book
            var adddBookCommand = new AddBookToLibraryCommand(_authorRepo, _mybookRepo);
            
            // execute
            var result = adddBookCommand.Handle(cmdParameters);
            
            // check results
            if (result.Succeeded)
            {
                lstLog.Items.Add("new book added");
            }
            else
            {
                lstLog.Items.AddRange(result.GetErrors().Select(error => error.Message).ToArray());
            }
        }
    }
}
