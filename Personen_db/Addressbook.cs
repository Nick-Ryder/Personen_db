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
            myPerson.FName = textBoxFname.Text;
            myPerson.LName = textBoxLname.Text;
            myPerson.Street = textBoxStreet.Text;
            myPerson.Number = textBoxNr.Text;
            myPerson.Plz = textBoxPlz.Text;
            myPerson.Location = textBoxLocation.Text;
            myPerson.Telephone = textBoxTelefon.Text;
            myPerson.Email = textBoxEmail.Text;
            //MessageBox.Show(myPerson.fName + myPerson.Lname + myPerson.street + myPerson.number + myPerson.plz + myPerson.location + myPerson.telefon + myPerson.email, "Hallo", MessageBoxButtons.OK);
            // Personendaten in mdf eintragen
            int index=myDB.Insert(myPerson);

            // Daten in Listview eintragen
            ListViewItem lvi;
            lvi = new ListViewItem(myPerson.FName);
            lvi.SubItems.Add(myPerson.LName);
            lvi.SubItems.Add(myPerson.Street);
            lvi.SubItems.Add(myPerson.Number);
            lvi.SubItems.Add(myPerson.Plz);
            lvi.SubItems.Add(myPerson.Location);
            lvi.SubItems.Add(myPerson.Telephone);
            lvi.SubItems.Add(myPerson.Email);
            lvi.SubItems.Add(index.ToString());
            listView1.Items.Add(lvi);
            ClearForm();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0) {
                int id_db = Convert.ToInt32(listView1.SelectedItems[0].SubItems[8].Text);               // id in db
                //MessageBox.Show(listView1.SelectedIndices[0].ToString(), "Hallo", MessageBoxButtons.OK);
                //MessageBox.Show(id_db.ToString(), "Hallo", MessageBoxButtons.OK);
                if (myDB.RemoveFromDb(id_db)) {
                    listView1.Items.RemoveAt(listView1.SelectedIndices[0]);
                    ClearForm();
                    insert.Enabled = false;
                    delete.Enabled = false;
                    change.Enabled = false;
                    Neu.Enabled = true;
                }
            }
        }

        private void Change_Click(object sender, EventArgs e)
        {
            ShowAddresses();
        }

        private void Neu_Click(object sender, EventArgs e)
        {
            ClearForm();
            insert.Enabled = true;
            delete.Enabled = false;
            change.Enabled = false;
            textBoxFname.Focus();
        }

        private void ShowAddresses()
        {
            List<Person> myAddresses = new List<Person>();
            myAddresses = myDB.ReadDB();
            foreach (Person entry in myAddresses)
            {
                ListViewItem lvi = new ListViewItem(entry.FName);
                lvi.SubItems.Add(entry.LName);
                lvi.SubItems.Add(entry.Street);
                lvi.SubItems.Add(entry.Number);
                lvi.SubItems.Add(entry.Plz);
                lvi.SubItems.Add(entry.Location);
                lvi.SubItems.Add(entry.Telephone);
                lvi.SubItems.Add(entry.Email);
                lvi.SubItems.Add(entry.Id.ToString());
                listView1.Items.Add(lvi);
            }
        }

        private void ListView1_ItemActivate(object sender, EventArgs e)             // Doppelklick
        {
            ListViewEntry_Selected();
            insert.Enabled = false;
            change.Enabled = true;
            delete.Enabled = true;
        }

        private void ListView1_ItemActivate(object sender, MouseEventArgs e)        // einfacher Klick
        {
            ListViewEntry_Selected();
            insert.Enabled = false;
            change.Enabled = true;
            delete.Enabled = true;
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
