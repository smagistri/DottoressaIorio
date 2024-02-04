window.jsLoadFeather = () => {
    'use strict'
    feather.replace({ 'aria-hidden': 'true' });
}

window.jsSaveAsFile = (filename, byteBase64) => {
    var link = document.createElement('a');
    link.download = filename;
    link.href = "data:application/pdf;base64," + byteBase64;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

window.jsCloseNavbar = () => {
    // Close the navbar using the Collapse API
    var sidebar = document.getElementById('sidebarMenu');
    if (sidebar.classList.contains('show')) {
        var bootstrapCollapse = new bootstrap.Collapse(sidebar);
        bootstrapCollapse.hide();
    }
}
