document.addEventListener("DOMContentLoaded", () => {

    const input = document.getElementById("search-input");
    const container = document.getElementById("jobsContainer");
    const categoryLinks = document.querySelectorAll(".category-link");

    let selectedCategory = "0";
    let timer;
    let formattedSalary = "Зарплата не указана";
   
    function updateJobs() {

        const query = encodeURIComponent(input.value);
        fetch(`/Home/Search?query=${query}&category=${selectedCategory}`)
            .then(response => response.json())
            .then(data => {
                container.innerHTML = "";
                if (data.length === 0) {
                    container.innerHTML = "<p>Ничего не нашлось</p>";
                    return;
                }
                data.forEach(job => {
                    
                    if (job.salary !== undefined && job.salary !== null) {
                        formattedSalary = job.salary.toLocaleString() + " ₽";
                    }
                    const date = job.createdAt.replace("T", " ");
                    container.innerHTML += `
                    <a href="/DetailInfo/Details/${job.id}" class="job-container__card-link">  
                        <div class="jobs-container__card">
                            <div class="jobs-container__card-info">
                                <div class="jobs-container__card-title">${job.title}</div>
                                <div class="jobs-container__card-details">
                                    <div class="jobs-container__card-location">
                                        <img src="/fonts/Location.svg" class="jobs-container__card-location-icon">
                                        <span class="jobs-container__card-location-text">${job.location}</span>
                                    </div>
                                    <div class="jobs-container__card-price">
                                        ${formattedSalary}
                                    </div>
                                </div>
                                <div class="jobs-container__card-description">
                                    <p class="jobs-container__card-description-text">Описание: ${job.description}</p>
                                </div>
                                <div class="jobs-container__card-date">
                                    <img src="/fonts/Calendar.svg" class="jobs-container__card-location-icon">
                                    <span class="jobs-container__card-views-text">${date}</span>
                                    <div class="jobs-container__card-views">
                                        <img src="/fonts/Eye.svg" class="jobs-container__card-location-icon">
                                        <span class="jobs-container__card-views-text">${job.views}</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                    `;
                });

            });
    }

    // поиск
    input.addEventListener("input", () => {

        clearTimeout(timer);

        timer = setTimeout(() => {
            updateJobs();
        }, 300);

    });

    // категории
    categoryLinks.forEach(link => {

        link.addEventListener("click", e => {

            e.preventDefault();

            categoryLinks.forEach(l => l.classList.remove("active"));

            link.classList.add("active");

            selectedCategory = link.dataset.id || "0";

            updateJobs();

        });

    });

});