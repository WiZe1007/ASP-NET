CREATE TABLE [dbo].[ParagraphStyles](
    [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [FontSize] NVARCHAR(50) NOT NULL,
    [Margin] NVARCHAR(50) NOT NULL,
    [Padding] NVARCHAR(50) NOT NULL
);

INSERT INTO [dbo].[ParagraphStyles] (FontSize, Margin, Padding)
VALUES 
('14px','10px','5px'),
('18px','15px','10px'),
('22px','20px','15px');
