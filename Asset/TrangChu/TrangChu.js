const listImg = document.querySelector('#img-banners');
const imgs = document.getElementsByClassName('img-banner');

const btnLeft = document.querySelector('.btn-left');
const btnRight = document.querySelector('.btn-right');


const dots = document.querySelectorAll('.dot');

//Biến đếm ảnhh
let current = 0;

let toRight = () => {
    if (current == imgs.length - 1) {
        current = 0;
    }
    else {
        current++;
    }
    let width = imgs[0].offsetWidth;
    listImg.style.transform = `translateX(${-width * current}px)`;
    document.querySelector('.active').classList.remove('active');
    document.querySelector('.dot-' + current).classList.add('active')
}

let toLeft = () => {
    if (current == 0) {
        current = imgs.length - 1;
    }
    else {
        current--;
    }
    let width = imgs[0].offsetWidth;
    listImg.style.transform = `translateX(${-width * current}px)`;
    document.querySelector('.active').classList.remove('active');
    document.querySelector('.dot-' + current).classList.add('active')
}

let autoChange = setInterval(toRight, 4000);


btnRight.addEventListener('click', () => {
    clearInterval(autoChange)
    toRight();
    autoChange = setInterval(toRight, 4000);
})

btnLeft.addEventListener('click', () => {
    clearInterval(autoChange)
    toLeft();
    autoChange = setInterval(toRight, 4000);
})


function dotClick(dot, index) {
    current = index;  //gán giá trị biến đếm ảnh = index (vị trí tương ứng của từng dot)
    let width = imgs[0].offsetWidth;
    listImg.style.transform = `translateX(${-width * current}px)`;

    // Cập nhật class active cho dot tương ứng
    document.querySelector('.active').classList.remove('active');
    dot.classList.add('active');

    // Reset lại autoChange
    clearInterval(autoChange);
    autoChange = setInterval(toRight, 4000);
}

// Gắn sự kiện click cho từng dot
dots.forEach((dot, index) => {
    dot.addEventListener('click', () => dotClick(dot, index));
});









