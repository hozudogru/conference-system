document.addEventListener('DOMContentLoaded', function () {
    const slides = Array.from(document.querySelectorAll('.wire-slide'));
    const dotsWrap = document.getElementById('wireSliderDots');
    const prevBtn = document.getElementById('wireSliderPrev');
    const nextBtn = document.getElementById('wireSliderNext');
    let currentSlideIndex = 0;
    let sliderTimer = null;

    const renderSlide = (index) => {
        if (!slides.length) return;
        currentSlideIndex = (index + slides.length) % slides.length;
        slides.forEach((slide, i) => slide.classList.toggle('is-active', i === currentSlideIndex));
        if (dotsWrap) {
            dotsWrap.querySelectorAll('button').forEach((dot, i) => dot.classList.toggle('is-active', i === currentSlideIndex));
        }
    };

    const startSlider = () => {
        if (slides.length < 2) return;
        stopSlider();
        sliderTimer = window.setInterval(() => renderSlide(currentSlideIndex + 1), 4500);
    };

    const stopSlider = () => {
        if (sliderTimer) {
            window.clearInterval(sliderTimer);
            sliderTimer = null;
        }
    };

    if (dotsWrap) {
        dotsWrap.addEventListener('click', (event) => {
            const button = event.target.closest('button[data-slide]');
            if (!button) return;
            renderSlide(parseInt(button.dataset.slide || '0', 10));
            startSlider();
        });
    }

    if (prevBtn) {
        prevBtn.addEventListener('click', () => {
            renderSlide(currentSlideIndex - 1);
            startSlider();
        });
    }

    if (nextBtn) {
        nextBtn.addEventListener('click', () => {
            renderSlide(currentSlideIndex + 1);
            startSlider();
        });
    }

    renderSlide(0);
    startSlider();

    const menu = document.getElementById('ajaxSidebarMenu');
    const defaultContent = document.getElementById('homeDefaultContent');
    const ajaxStage = document.getElementById('ajaxContentStage');
    const ajaxHost = document.getElementById('ajaxContentHost');
    const ajaxTitle = document.getElementById('ajaxStageTitle');
    const backButton = document.getElementById('ajaxBackHome');

    if (!menu || !defaultContent || !ajaxStage || !ajaxHost || !ajaxTitle) {
        return;
    }

    const setActive = (button) => {
        menu.querySelectorAll('.wire-menu-item').forEach((item) => item.classList.remove('active'));
        if (button) {
            button.classList.add('active');
        }
    };

    const showHome = () => {
        defaultContent.style.display = '';
        ajaxStage.classList.add('ajax-stage-hidden');
        ajaxHost.innerHTML = '';
        ajaxTitle.textContent = 'Sayfa';
        const homeButton = menu.querySelector('[data-home-tab="home"]');
        setActive(homeButton);
    };

    const showLoading = () => {
        ajaxHost.innerHTML = '<div class="ajax-loader-state"><span class="loader-ring"></span><span>İçerik yükleniyor...</span></div>';
    };

    const loadAjaxPage = async (slug, title, button) => {
        if (!slug) return;

        setActive(button);
        defaultContent.style.display = 'none';
        ajaxStage.classList.remove('ajax-stage-hidden');
        ajaxTitle.textContent = title || 'Sayfa';
        showLoading();

        try {
            const response = await fetch(`/sayfa/icerik/${slug}`, {
                headers: { 'X-Requested-With': 'XMLHttpRequest' }
            });

            const html = await response.text();
            ajaxHost.innerHTML = html;
        } catch (error) {
            ajaxHost.innerHTML = '<div class="glass-wire" style="padding:24px;"><h3 style="margin:0 0 8px;color:#6f4a12;">İçerik yüklenemedi</h3><p style="margin:0;color:#7f6b4f;">Bağlantı sırasında bir sorun oluştu.</p></div>';
        }
    };

    menu.addEventListener('click', function (event) {
        const button = event.target.closest('.wire-menu-item');
        if (!button) return;

        if (button.dataset.homeTab === 'home') {
            showHome();
            return;
        }

        const slug = button.dataset.ajaxSlug;
        const title = button.dataset.ajaxTitle;
        if (!slug) return;

        loadAjaxPage(slug, title, button);
    });

    if (backButton) {
        backButton.addEventListener('click', showHome);
    }
});
