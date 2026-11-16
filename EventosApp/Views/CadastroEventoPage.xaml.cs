using EventosApp.Models;

namespace EventosApp.Views;

public partial class CadastroEventoPage : ContentPage
{
    private readonly Evento _evento = new();

    public CadastroEventoPage()
    {
        InitializeComponent();
        BindingContext = _evento;
    }

    private async void OnCadastrarClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_evento.Nome) ||
            string.IsNullOrWhiteSpace(_evento.Local))
        {
            await DisplayAlert("Atenção", "Informe o nome e o local do evento.", "OK");
            return;
        }

        if (_evento.DataTermino < _evento.DataInicio)
        {
            await DisplayAlert("Atenção", "A data de término não pode ser anterior ao início.", "OK");
            return;
        }

        if (_evento.NumeroParticipantes <= 0)
        {
            await DisplayAlert("Atenção", "O número de participantes deve ser maior que zero.", "OK");
            return;
        }

        if (_evento.CustoPorParticipante < 0)
        {
            await DisplayAlert("Atenção", "O custo por participante não pode ser negativo.", "OK");
            return;
        }

        await Navigation.PushAsync(new ResumoEventoPage(_evento));
    }
}