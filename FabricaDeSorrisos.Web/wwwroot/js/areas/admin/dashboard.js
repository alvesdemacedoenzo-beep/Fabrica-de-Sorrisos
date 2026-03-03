document.addEventListener("DOMContentLoaded", function () {

    const palette = ['#F66CD8', '#F3DD24', '#00BAF6', '#00C709', '#000269'];

    // --- GRÁFICO 1: CATEGORIAS (COLUNAS) ---
    const colunasContainer = document.querySelector("#chartColunas");
    if (colunasContainer) {
        const labels = colunasContainer.dataset.labels ? colunasContainer.dataset.labels.split(',') : [];
        const values = colunasContainer.dataset.values ? colunasContainer.dataset.values.split(',').map(Number) : [];

        var optionsColunas = {
            series: [{ name: 'Vendas', data: values }],
            chart: { height: 350, type: 'bar', toolbar: { show: false }, fontFamily: 'Inter, sans-serif' },
            colors: palette,
            plotOptions: { bar: { columnWidth: '50%', distributed: true, borderRadius: 6 } },
            dataLabels: { enabled: false },
            legend: { show: false },
            xaxis: { categories: labels, labels: { style: { fontSize: '12px', fontWeight: 600 } } },
            grid: { borderColor: '#f1f1f1' }
        };

        var chartCol = new ApexCharts(colunasContainer, optionsColunas);
        chartCol.render();
    }

    // --- GRÁFICO 2: TOP BRINQUEDOS (MUDOU PARA BARRAS HORIZONTAIS) ---
    const brinquedosContainer = document.querySelector("#chartBrinquedos");
    if (brinquedosContainer) {
        const labels = brinquedosContainer.dataset.labels ? brinquedosContainer.dataset.labels.split(',') : [];
        const values = brinquedosContainer.dataset.values ? brinquedosContainer.dataset.values.split(',').map(Number) : [];

        var optionsBrinquedos = {
            series: [{ name: 'Qtd Vendida', data: values }],
            chart: { height: 350, type: 'bar', toolbar: { show: false }, fontFamily: 'Inter, sans-serif' },
            colors: ['#00BAF6'], // Azul da fábrica
            plotOptions: {
                bar: {
                    horizontal: true, // Barras deitadas para ler melhor os nomes
                    borderRadius: 4,
                    barHeight: '50%'
                }
            },
            dataLabels: { enabled: true, style: { fontSize: '12px' } },
            xaxis: { categories: labels },
            grid: { borderColor: '#f1f1f1' }
        };

        var chartBrinq = new ApexCharts(brinquedosContainer, optionsBrinquedos);
        chartBrinq.render();
    }
});