import re
words_to_remove = ['rot','blau','gelb','grün','gruen','weiß','weiss','schwarz','grau','rosa','pink','lila','braun','gold','silber','orange','dunkelgrau','beige','dunkel','hell']
material_raw:str
material_preprocessed:str

def preprocess(material_name):
    old = material_name;
    material_name = re.sub(r'[\d]{1,3}[\s_-]+[\d]{1,3}[\s_-]+[\d]{1,3}','',material_name);
    #remove special characters eg ,.-;:_
    material_name = (" ".join(re.findall(r"[A-Za-z0-9üäöÜÄÖßóåéèÉÈÓ/]*", material_name))).replace("  "," ");
    #remove size and add afterwards as one
    size_threedimensional = re.compile('[\d]{1,4}[\s]?[x][\s]?[\d]{1,4}([\s]?[x][\s]?[\d]{1,4})?');
    size_twodimensional = re.compile('[\d]{1,4}[\s]?[x][\s]?[\d]{1,4}');
    # size = re.findall('[\d]{1,4}[\s]?[x][\s]?[\d]{1,4}[\s]?[x][\s]?[\d]{1,4}', material_name);
    size = re.findall('[\d]{1,4}[\s]?[x][\s]?[\d]{1,4}([\s]?[x][\s]?[\d]{1,4})?', material_name);
    if len(size) <= 0:
        size = re.findall(size_twodimensional, material_name);
    material_name = re.sub(size_threedimensional,'',material_name);
    for word in words_to_remove:
        if word in material_name.lower():
            index = material_name.lower().index(word);
            if index >= 0:
                material_name = material_name[:index] + material_name[index+len(word):];
    while('  ' in material_name):
        material_name = material_name.replace('  ',' ')
    material_name = material_name.strip(); 
    print(f"'{old}' -> '{material_name}'")
    return material_name;

print(__name__);
if __name__ == "__main__":
    try:
        material_raw
    except NameError:
        material_raw = input("Text to preprocess: ")
    material_preprocessed = preprocess(material_raw)
    print(material_preprocessed)