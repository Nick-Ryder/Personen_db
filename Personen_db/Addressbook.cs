using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Personen_db
{
    public partial class Addressbook : Form
    {
        Person myPerson = new Person();
        AddressDB myDB = new AddressDB();
        public Addressbook()
        {
            InitializeComponent();
            ShowAddresses();
        }

        public void Insert_Click(object sender, EventArgs e)
        {
            myPerson.fName = textBoxFname.Text;
            myPerson.lName = textBoxLname.Text;
            myPerson.street = textBoxStreet.Text;
            myPerson.number = textBoxNr.Text;
            myPerson.plz = textBoxPlz.Text;
            myPerson.location = textBoxLocation.Text;
            myPerson.telefon = textBoxTelefon.Text;
            myPerson.email = textBoxEmail.Text;
            //MessageBox.Show(myPerson.fName + myPerson.Lname + myPerson.street + myPerson.number + myPerson.plz + myPerson.location + myPerson.telefon + myPerson.email, "Hallo", MessageBoxButtons.OK);
            // Personendaten in mdf eintragen
            myDB.Insert(myPerson);

            // Daten in Listview eintragen
            ListViewItem lvi;
            lvi = new ListViewItem(myPerson.fName);
            lvi.SubItems.Add(myPerson.lName);
            lvi.SubItems.Add(myPerson.street);
            lvi.SubItems.Add(myPerson.number);
            lvi.SubItems.Add(myPerson.plz);
            lvi.SubItems.Add(myPerson.location);
            lvi.SubItems.Add(myPerson.telefon);
            lvi.SubItems.Add(myPerson.email);
            lvi.SubItems.Add(myPerson.id.ToString());
            listView1.Items.Add(lvi);
            ClearForm();
        }

        private void Change_Click(object sender, EventArgs e)
        {
            ShowAddresses();
        }

        private void ShowAddresses()
        {
            List<Person> myAddresses = new List<Person>();
            myAddresses = myDB.readDB();
            foreach (Person entry in myAddresses)
            {
                ListViewItem lvi = new ListViewItem(entry.fName);
                lvi.SubItems.Add(entry.lName);
                lvi.SubItems.Add(entry.street);
                lvi.SubItems.Add(entry.number);
                lvi.SubItems.Add(entry.plz);
                lvi.SubItems.Add(entry.location);
                lvi.SubItems.Add(entry.telefon);
                lvi.SubItems.Add(entry.email);
                lvi.SubItems.Add(entry.id.ToString());
                listView1.Items.Add(lvi);
            }
        }

        private void listView1_ItemActivate(object sender, EventArgs e)             // Doppelklick
        {
            ListViewEntry_Selected();
        }

        private void listView1_ItemActivate(object sender, MouseEventArgs e)        // einfacher Klick
        {
            ListViewEntry_Selected();
        }

        private void Neu_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            textBoxFname.Text = "";
            textBoxLname.Text = "";
            textBoxStreet.Text = "";
            textBoxNr.Text = "";
            textBoxPlz.Text = "";
            textBoxLocation.Text = "";
            textBoxTelefon.Text = "";
            textBoxEmail.Text = "";
            insert.Enabled = true;
        }

        private void ListViewEntry_Selected()
        {
            //MessageBox.Show(listView1.SelectedIndices[0].ToString(), "Hallo", MessageBoxButtons.OK); // Zeilennummer des selektierten Eintrags
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem lvi = listView1.SelectedItems[0];
                textBoxFname.Text = lvi.Text;
                textBoxLname.Text = lvi.SubItems[1].Text;
                textBoxStreet.Text = lvi.SubItems[2].Text;
                textBoxNr.Text = lvi.SubItems[3].Text;
                textBoxPlz.Text = lvi.SubItems[4].Text;
                textBoxLocation.Text = lvi.SubItems[5].Text;
                textBoxTelefon.Text = lvi.SubItems[6].Text;
                textBoxEmail.Text = lvi.SubItems[7].Text;
                insert.Enabled = false;
            }
        }
    }
}
