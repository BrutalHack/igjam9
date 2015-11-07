for image in *.png
do
	convert $image -brightness-contrast -80 $image
done
