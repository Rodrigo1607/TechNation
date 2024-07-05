$(document).ready((e) => {

    $('#total_notas_emitidas').text('123');
    $('#total_notas_sem_cobranca').text('23');
    $('#total_notas_vencidas').text('100');
    $('#total_notas_irao_vencer').text('30');
    $('#total_notas_pagas').text('50');
});

const graficoInadimplecia = document.getElementById('grafico_barras_inadimplencia');
const graficoReceitaRecebida = document.getElementById('grafico_barras_receitas-recebidas');

new Chart(graficoInadimplecia, {
    type: 'bar',
    data: {
        labels: ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
        datasets: [{
            label: 'Evolução de inadimplência',
            data: [12, 19, 3, 5, 2, 3, 16, 8, 12, 20, 10, 5],
            borderWidth: 1,
            backgroundColor: '#BF6E50',
        }]
    },
    options: {
        indexAxis: 'y',
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
});

new Chart(graficoReceitaRecebida, {
    type: 'bar',
    data: {
        labels: ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
        datasets: [{
            label: 'Evolução de receita recebida',
            data: [12, 19, 3, 5, 2, 3, 10, 18, 9, 5, 13, 6],
            borderWidth: 1,
            backgroundColor: '#F2B6A0',
        }]
    },
    options: {
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
});

//Exemplo para criação da tabela utilizando dados vindos do back-end
$.ajax({
    url: '',
    type: 'post',
    dataSrc: '',
    success: (e) => {

        new Chart(graficoReceitaRecebida, {
            type: 'bar',
            data: {
                labels: ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
                datasets: [{
                    label: 'Evolução de receita recebida',
                    data: [e.janeiro, e.fevereiro, e.marco, e.abril, e.maio, e.junho, e.julho, e.agosto, e.setembro, e.outubro, e.novembro, e.dezembro],
                    borderWidth: 1
                }]
            },
            options: {
                indexAxis: 'y',
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });
    }
});

