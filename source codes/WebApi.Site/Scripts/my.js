
function buildPropertyTree(obj) {
    var propertyList = $('<ul/>');
    for (var p in obj) {
        var propertyItem = $('<li/>');

        var val = obj[p];

        if (typeof val == 'object') {
            propertyItem.text(p);
            buildPropertyTree(val).appendTo(propertyItem);
        } else {
            propertyItem.text(p + '=' + val);
        }
        propertyItem.appendTo(propertyList);
    }
    return propertyList;
}
