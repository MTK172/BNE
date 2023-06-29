window.addEventListener('DOMContentLoaded', function() {
    const filterInput = document.getElementById('filter');
    const cardWrapper = document.querySelector('.card-wrapper');
  
    filterInput.addEventListener('input', filterProducts);
  
    function filterProducts() {
      const filterValue = filterInput.value.toLowerCase();
      const cardItems = cardWrapper.getElementsByClassName('card-item');
  
      for (let i = 0; i < cardItems.length; i++) {
        const title = cardItems[i].getElementsByTagName('h3')[0].textContent.toLowerCase();
        const description = cardItems[i].getElementsByTagName('p')[0].textContent.toLowerCase();
        const shouldDisplay = title.includes(filterValue) || description.includes(filterValue);
        cardItems[i].style.display = shouldDisplay ? 'block' : 'none';
      }
    }
  });
  