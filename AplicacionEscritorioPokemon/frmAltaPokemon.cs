using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using datos;
using dominio;
using System.Configuration;



namespace AplicacionEscritorioPokemon
{
    public partial class frmAltaPokemon : Form
    {
        private Pokemon pokemon = null;
        private OpenFileDialog archivo = null;
        public frmAltaPokemon()
        {
            InitializeComponent();
        }

        public frmAltaPokemon(Pokemon pokemon)
        {
            InitializeComponent();
            this.pokemon = pokemon;
            Text = "Modificar Pokemon";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            
            PokemonDatos datosPokemon = new PokemonDatos();

            try
            {
                if(pokemon == null)
                    pokemon = new Pokemon();

                pokemon.Numero= int.Parse(txtNumero.Text);
                pokemon.Nombre = txtNombre.Text;
                pokemon.Descripcion= txtDescripcion.Text;
                pokemon.UrlImagen = txtUrlImagen.Text;
                pokemon.Tipo = (Elemento)cmbTipo.SelectedItem;
                pokemon.Debilidad = (Elemento)cmbDeblidad.SelectedItem;

                if(pokemon.Id != 0)
                {
                    datosPokemon.modificar(pokemon);
                    MessageBox.Show("Modificado correctamente");
                }
                else
                {
                    datosPokemon.agregar(pokemon);
                    MessageBox.Show("Agregado correctamente");
                }
                if (archivo != null && !(txtUrlImagen.Text.ToUpper().Contains("HTTP")))
                    File.Copy(archivo.FileName, ConfigurationManager.AppSettings["carpeta-imagen"] + archivo.SafeFileName);


                Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void frmAltaPokemon_Load(object sender, EventArgs e)
        {
            ElementoDatos datosElemento = new ElementoDatos();
            try
            {
                cmbTipo.DataSource = datosElemento.listar();
                cmbTipo.ValueMember = "id";
                cmbTipo.DisplayMember = "Descripcion";
                cmbDeblidad.DataSource = datosElemento.listar();
                cmbDeblidad.ValueMember = "Id";
                cmbDeblidad.DisplayMember = "Descripcion";

                if(pokemon != null)
                {
                    txtNumero.Text = pokemon.Numero.ToString();
                    txtNombre.Text = pokemon.Nombre;
                    txtDescripcion.Text = pokemon.Descripcion;
                    txtUrlImagen.Text = pokemon.UrlImagen;
                    cargarImagen(pokemon.UrlImagen);
                    cmbTipo.SelectedValue = pokemon.Tipo.Id;
                    cmbDeblidad.SelectedValue = pokemon.Debilidad.Id;

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtUrlImagen.Text);
        }

        private void cargarImagen( string imagen)
        {
            try
            {
                pbxPokemonNuevo.Load(imagen);

            }
            catch (Exception)
            {

                pbxPokemonNuevo.Load("https://uning.es/wp-content/uploads/2016/08/ef3-placeholder-image.jpg"); ;
            }

        }

        private void btnLevantarImagen_Click(object sender, EventArgs e)
        {
            archivo = new OpenFileDialog();
            archivo.Filter = "jpg|*.jpg;|png|*.png";
            if(archivo.ShowDialog() == DialogResult.OK)
            {
                txtUrlImagen.Text = archivo.FileName;
                cargarImagen(archivo.FileName);
               
            }
        }
    }
}

