for image in *.png
do
	convert $image -transparent black $image
done
