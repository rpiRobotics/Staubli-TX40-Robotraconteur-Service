function I=CameraBitmapToImage(c)

if(c.pixeltype==uint8(1))
    I=reshape(c.data,c.width,c.height)';
    return;
end

if (c.pixeltype==uint8(2))
    I=reshape(typecast(c.data,'uint16'),c.width,c.height)';
    return;
end

if (c.pixeltype==uint8(3))
    I=zeros([c.height,c.width,3],'uint8');
    I(:,:,3)=reshape(c.data(1:3:end),c.width,c.height)';
    I(:,:,2)=reshape(c.data(2:3:end),c.width,c.height)';
    I(:,:,1)=reshape(c.data(3:3:end),c.width,c.height)';
    return;
end


if (c.pixeltype==4)
    I=zeros([c.height,c.width,3],'uint8');
    I(:,:,3)=reshape(c.data(1:4:end),c.width,c.height)';
    I(:,:,2)=reshape(c.data(2:4:end),c.width,c.height)';
    I(:,:,1)=reshape(c.data(3:4:end),c.width,c.height)';
    return;
    
end


error('Unknown CameraBitmap pixel type');