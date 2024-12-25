function updateTime() {
    const now = new Date();

    const daysOfWeek = ['Chủ nhật', 'Thứ hai', 'Thứ ba', 'Thứ tư', 'Thứ năm', 'Thứ sáu', 'Thứ bảy'];
    const months = ['Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6', 'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12'];

    const year = now.getFullYear();
    const month = months[now.getMonth()];
    const day = now.getDate();
    const dayOfWeek = daysOfWeek[now.getDay()];
    const hours = now.getHours();
    const minutes = now.getMinutes();
    const seconds = now.getSeconds();

    const timeString = `${hours}:${minutes}:${seconds}
    ${dayOfWeek}, Ngày ${day} ${month} ${year}`;

    document.getElementById("current-time").textContent = timeString;
}

setInterval(updateTime, 1); 
