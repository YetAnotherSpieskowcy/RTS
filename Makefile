UML = plantuml
TEX = pdflatex

all:	deploy 

uml:
	$(UML) uml/*.uml

%.tex: uml
	$(TEX) $@
deploy: main.tex
