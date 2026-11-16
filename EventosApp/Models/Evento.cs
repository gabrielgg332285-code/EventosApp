using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EventosApp.Models;

public class Evento : INotifyPropertyChanged
{
    private string _nome = string.Empty;
    private DateTime _dataInicio = DateTime.Today;
    private DateTime _dataTermino = DateTime.Today;
    private int _numeroParticipantes;
    private string _local = string.Empty;
    private decimal _custoPorParticipante;

    public string Nome
    {
        get => _nome;
        set => Set(ref _nome, value);
    }

    public DateTime DataInicio
    {
        get => _dataInicio;
        set
        {
            if (Set(ref _dataInicio, value))
            {
                if (_dataTermino < _dataInicio)
                    DataTermino = _dataInicio;
                Atualizar(nameof(Duracao), nameof(DuracaoDias), nameof(DuracaoTexto));
            }
        }
    }

    public DateTime DataTermino
    {
        get => _dataTermino;
        set
        {
            if (Set(ref _dataTermino, value))
                Atualizar(nameof(Duracao), nameof(DuracaoDias), nameof(DuracaoTexto));
        }
    }

    public int NumeroParticipantes
    {
        get => _numeroParticipantes;
        set
        {
            if (Set(ref _numeroParticipantes, value))
                Atualizar(nameof(CustoTotal));
        }
    }

    public string Local
    {
        get => _local;
        set => Set(ref _local, value);
    }

    public decimal CustoPorParticipante
    {
        get => _custoPorParticipante;
        set
        {
            if (Set(ref _custoPorParticipante, value))
                Atualizar(nameof(CustoTotal));
        }
    }

    // ----- Calculados -----
    public TimeSpan Duracao => DataTermino.Date - DataInicio.Date;
    public int DuracaoDias => Duracao.Days + 1;
    public string DuracaoTexto =>
        $"{DuracaoDias} dia(s)  (TimeSpan: {Duracao:d\\.hh\\:mm\\:ss})";
    public decimal CustoTotal => NumeroParticipantes * CustoPorParticipante;

    // ----- INotifyPropertyChanged -----
    public event PropertyChangedEventHandler? PropertyChanged;

    private bool Set<T>(ref T campo, T valor, [CallerMemberName] string? nome = null)
    {
        if (EqualityComparer<T>.Default.Equals(campo, valor)) return false;
        campo = valor;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
        return true;
    }

    private void Atualizar(params string[] nomes)
    {
        foreach (var n in nomes)
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
    }
}