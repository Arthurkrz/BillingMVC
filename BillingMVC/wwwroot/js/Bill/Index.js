$(document).ready(function () {
    if (typeof jQuery === "undefined") {
        console.error("jQuery is not loaded!");
        return;
    }

    const totalInReais = parseFloat
        ('@ViewBag.TotalInReais.ToString("F2", System.Globalization.CultureInfo.InvariantCulture)') || 0;
    const totalInEuros = parseFloat
        ('@ViewBag.TotalInEuros.ToString("F2", System.Globalization.CultureInfo.InvariantCulture)') || 0;
    const totalAmountElement = document.getElementById('totalAmount');
    const currencySelect = document.getElementById('currencySelect');

    function updateTotalDisplay(currency) {
        if (!totalAmountElement) return;
        if (currency === 'BRL') {
            totalAmountElement.innerHTML =
                `Valor Total das Despesas - R$ ${totalInReais.toFixed(2)}`;
        } else if (currency === 'EUR') {
            totalAmountElement.innerHTML =
                `Valor Total das Despesas - € ${totalInEuros.toFixed(2)}`;
        }
    }

    if (currencySelect) {
        currencySelect.value = 'BRL';
        updateTotalDisplay('BRL');
        currencySelect.addEventListener('change', function () {
            updateTotalDisplay(this.value);
        });
    }

    $('#createBillModal').on('hidden.bs.modal', function () {
        setTimeout(function () {
            $("#addBillButton").focus();
        }, 100);
    });
});

function enableValidation() {
    var form = $("#createBillForm");
    if (form.length === 0) {
        console.error("Form not found.");
    }
    $.validator.unobtrusive.parse(form);
}

enableValidation();

$(document).on("click", "#saveBillButton", function (e) {
    e.preventDefault();

    let form = $("#createBillForm");
    if (form.length === 0) {
        console.error("Form not found.");
        return
    }

    form.validate();

    if (!form.valid()) {
        console.warn("Form validation failed.");
        return;
    }

    $.ajax({
        url: "/Create/Bill",
        type: "POST",
        data: form.serialize(),
        success: function (response) {
            if (response.success) {
                $("#createBillModal").modal("hide");
                location.reload();
            } else {
                let errorContainer = $("#errorMessages");
                x
                if (response.errors && response.errors.length > 0) {
                    errorContainer.html(response.errors.join("<br>"));
                    errorContainer.removeClass("d-none").show();
                } else {
                    errorContainer.html("Erro desconhecido.");
                    errorContainer.removeClass("d-none").show();
                }
            }
        },
        error: function () {
            let errorContainer = $("#errorMessages");
            errorContainer.html("Erro ao processar solicitação.");
            errorContainer.removeClass("d-none").show();
        }
    });
});

$(document).on("click", "[id^='saveEditButton-']", function (e) {
    e.preventDefault();

    let buttonId = $(this).attr("id");
    let billId = buttonId.replace("saveEditButton-", "");

    let form = $("#editBillForm-" + billId);

    form.validate();

    if (!form.valid()) {
        return;
    }

    $.ajax({
        url: "/Update/Bill",
        type: "POST",
        data: form.serialize(),
        success: function (response) {
            if (response.success) {
                $("#editModal-" + billId).modal("hide");
                location.reload();
            } else {
                $("#editErrorMessages-" + billId)
                    .html(response.errors.join("<br>"))
                    .removeClass("d-none").show();
            }
        },
        error: function () {
            $("#editErrorMessages-" + billId)
                .html("Erro ao processar solicitação.")
                .removeClass("d-none").show();
        }
    });
});