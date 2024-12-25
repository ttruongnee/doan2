function EventInputTypeNumber() {
    const inputSoLuong = document.querySelectorAll('.input-soluong');
    const buttonUp = document.querySelectorAll('.button-up');
    const buttonDown = document.querySelectorAll('.button-down');

    // Xử lý sự kiện cho các input
    if (inputSoLuong.length > 0) {
        inputSoLuong.forEach(input => {
            input.addEventListener('input', function () {
                input.value = input.value.replace(/[^0-9]/g, '');
            });

            input.addEventListener('keydown', function (event) {
                if (event.key === 'Enter') {
                    input.blur();
                }
            });

            input.addEventListener('blur', function () {
                if (input.value == '') {
                    input.value = 1;
                }
            });
        });
    }

    // Xử lý sự kiện cho buttonUp
    if (buttonUp.length > 0) {
        buttonUp.forEach((button, index) => {
            button.addEventListener('click', function () {
                const input = inputSoLuong[index];
                if (input) {
                    input.value = parseInt(input.value) + 1; // Tăng số lượng
                }
            });
        });
    }

    // Xử lý sự kiện cho buttonDown
    if (buttonDown.length > 0) {
        buttonDown.forEach((button, index) => {
            button.addEventListener('click', function () {
                const input = inputSoLuong[index];
                if (input && parseInt(input.value) > 1) {
                    input.value = parseInt(input.value) - 1; // Giảm số lượng (giới hạn không dưới 1)
                }
            });
        });
    }
}

EventInputTypeNumber(); 